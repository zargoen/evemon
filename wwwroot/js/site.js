(function ($) {
    "use strict";

    // Hackish solution to https://github.com/twitter/bootstrap/issues/1768
    // where fixed-top navbar overlaps the jump link position
    $(window).load(function () {
        if ($(".navbar.navbar-fixed-top").length > 0) {
            var shiftWindow = function () {
                scrollBy(0, -$(".navbar").height() - 5);
            };
            if (location.hash) {
                setTimeout(shiftWindow, 1);
            }
        }
    });

    // Google Analytics
    (function (i, s, o, g, r, a, m) {
        i["GoogleAnalyticsObject"] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments);
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g;
        m.parentNode.insertBefore(a, m);
    })(window, document, "script", "//www.google-analytics.com/analytics.js", "ga");

    ga("create", "UA-71610557-4", "auto");
    ga("send", "pageview");

    // Fix for IE on WP8
    if ("-ms-user-select" in document.documentElement.style && navigator.userAgent.match(/IEMobile/)) {
        var msViewportStyle = document.createElement("style");
        msViewportStyle.appendChild(
			document.createTextNode("@-ms-viewport{width:auto!important}")
		);
        document.getElementsByTagName("head")[0].appendChild(msViewportStyle);
    }

    /******************************************
    ********** Helper functions **************
    ******************************************/
    // Get feed generic function
    var getFeed = function (url, element, defaultText) {
        $.get(url)
            .done(function (data) {
                try {
                    defaultText = marked(data);
                } catch (e) {
                    console.log(e);
                }
            })
            .fail(function (e) {
                console.log(e);
            })
            .always(function () {
                if (element) {
                    element.html(defaultText);
                }
            });
    };

    // Version comparer for finding the highest released version
    // Many thanks to: @copyright by Jon Papaioannou (["john", "papaioannou"].join(".") + "@gmail.com")
    // https://gist.github.com/TheDistantSea/8021359
    var versionComparer = function (v1, v2, options) {
        var lexicographical = options && options.lexicographical,
            zeroExtend = options && options.zeroExtend,
            v1Parts = v1.split("."),
            v2Parts = v2.split(".");

        function isValidPart(x) {
            return (lexicographical ? /^\d+[A-Za-z]*$/ : /^\d+$/).test(x);
        }

        if (!v1Parts.every(isValidPart) || !v2Parts.every(isValidPart)) {
            return NaN;
        }

        if (zeroExtend) {
            while (v1Parts.length < v2Parts.length) v1Parts.push("0");
            while (v2Parts.length < v1Parts.length) v2Parts.push("0");
        }

        if (!lexicographical) {
            v1Parts = v1Parts.map(Number);
            v2Parts = v2Parts.map(Number);
        }

        for (var i = 0; i < v1Parts.length; ++i) {
            if (v2Parts.length === i) {
                return 1;
            }

            if (v1Parts[i] === v2Parts[i]) {
                continue;
            }
            else if (v1Parts[i] > v2Parts[i]) {
                return 1;
            }
            else {
                return -1;
            }
        }

        if (v1Parts.length !== v2Parts.length) {
            return -1;
        }

        return 0;
    }

    // Gets the version text of the specified element
    var getVersionText = function (e) {
        return e.find("version").text();
    }

    // Gets the version in the format of 'major.minor.build'
    var getVersionWithoutRevision = function (version) {
        return version.substr(0, version.lastIndexOf("."));
    }

    // Gets the installer link of the release
    var getInstallerLinkOf = function (release) {
        return release.find("autopatchurl").text();
    }

    // Initilaizes the version text
    var versionText = "Unknown";

    // Occurs when a call to a uri succeeds
    var success = function (data) {
        try {
            var xmlDoc = undefined;
            try {
                xmlDoc = $.type(data) === $.type(new Object())
                    ? new XMLSerializer().serializeToString(data)
                    : $.parseXML(data);
            }
            catch (exc) {
                console.log(exc);
            }
            if (!xmlDoc)
                return;
            var releases = $(xmlDoc).find("release");
            if (releases && releases.length > 0) {
                releases.each(function (i, elem) {
                    var v1 = getVersionText($(elem)),
                        v2 = getVersionText($(elem.nextElementSibling));
                    if (!elem.nextElementSibling) {
                        if (versionText === "Unknown") {
                            versionText = getVersionWithoutRevision(v1);
                        }
                        return;
                    }
                    versionText = getVersionWithoutRevision(v1);
                    if (versionComparer(v1, v2, { lexicographical: true }) < 0) {
                        versionText = getVersionWithoutRevision(v2);
                    }
                });
            } else {
                versionText = getVersionWithoutRevision(getVersionText($(xmlDoc)));
            }
            if (!isMobile.any) {
                var installerLink;
                if (releases && releases.length > 0) {
                    releases.each(function (i, elem) {
                        if (!elem.nextElementSibling) {
                            if (installerLink === undefined) {
                                installerLink = getInstallerLinkOf($(elem));
                            }
                            return;
                        }
                        var v1 = getVersionText($(elem)),
                            v2 = getVersionText($(elem.nextElementSibling));
                        installerLink = getInstallerLinkOf($(elem));
                        if (versionComparer(v1, v2, { lexicographical: true }) < 0) {
                            installerLink = getInstallerLinkOf($(elem.nextElementSibling));
                        }
                    });
                } else {
                    installerLink = $(xmlDoc).find("newest autopatchurl").text();
                }
                if ($("#installer")) {
                    $("#installer").attr("href", installerLink);
                }
                if ($("#binaries")) {
                    $("#binaries").attr("href", installerLink.replace("install", "binaries").replace("exe", "zip"));
                }
            }
        } catch (ex) {
            console.log(ex);
        }

        if ($("#version")) {
            $("#version").text(versionText);
        }
    }

    /**************************************************************/

    // Occurs when document is ready
    $(function () {
        // Display js enabled sections
        $(".no-js").hide();
        $(".js").show();

        // Display the download section only for Desktops
        $(isMobile.any ? ".onlyDesktop" : ".onlyMobile").hide();

        // Gets the latest news feed
        getFeed("https://raw.githubusercontent.com/evemondevteam/evemon/gh-pages/mds/latestNews.md",
            $(".latest-news"),
            "Unable to get the latest news.");

        // Gets the change log feed
        getFeed("https://raw.githubusercontent.com/evemondevteam/evemon/gh-pages/mds/changeLog.md",
            $(".change-log"),
            "Unable to get the change logs.");


        // Gets the latest semantic version
        (function () {
            $.get("https://bitbucket.org/EVEMonDevTeam/evemon/wiki/updates/patch-emergency.xml")
                .done(success)
                .fail(function (e) {
                    console.log(e);
                    $.get("https://bitbucket.org/EVEMonDevTeam/evemon/wiki/updates/patch.xml")
                        .done(success)
                        .fail(function (ex) {
                            console.log(ex);
                            // Really hacking way to support Safari (don't like it at all)
                            $.get("https://bytebucket.org/EVEMonDevTeam/evemon/wiki/updates/patch.xml")
                                .done(success)
                                .fail(function (exc) {
                                    if ($("#version"))
                                        $("#version").text(versionText);
                                    console.log(exc);
                                });
                        });
                });
        })();

        // Adjust copyright year
        (function () {
            var startYear = $("#year") && new Date($("#year").text()).getUTCFullYear();
            var currentYear = new Date().getUTCFullYear();
            if (startYear && startYear < currentYear) {
                $("#year").text(startYear + " - " + currentYear);
            }
        })();
    });
})(jQuery)
