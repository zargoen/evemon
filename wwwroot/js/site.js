(function ($) {
    // Google Analytics
    (function (i, s, o, g, r, a, m) {
        i["GoogleAnalyticsObject"] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments);
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g;
        m.parentNode.insertBefore(a, m);
    })(window, document, "script", "//www.google-analytics.com/analytics.js", "ga");

    ga("create", "UA-26851280-5", "auto");
    ga("send", "pageview");

    // Fix for IE on WP8
    if ("-ms-user-select" in document.documentElement.style && navigator.userAgent.match(/IEMobile/)) {
        var msViewportStyle = document.createElement("style");
        msViewportStyle.appendChild(
			document.createTextNode("@-ms-viewport{width:auto!important}")
		);
        document.getElementsByTagName("head")[0].appendChild(msViewportStyle);
    }
    
    // Minified version of isMobile
    !function (a) { var b = /iPhone/i, c = /iPod/i, d = /iPad/i, e = /(?=.*\bAndroid\b)(?=.*\bMobile\b)/i, f = /Android/i, g = /(?=.*\bAndroid\b)(?=.*\bSD4930UR\b)/i, h = /(?=.*\bAndroid\b)(?=.*\b(?:KFOT|KFTT|KFJWI|KFJWA|KFSOWI|KFTHWI|KFTHWA|KFAPWI|KFAPWA|KFARWI|KFASWI|KFSAWI|KFSAWA)\b)/i, i = /IEMobile/i, j = /(?=.*\bWindows\b)(?=.*\bARM\b)/i, k = /BlackBerry/i, l = /BB10/i, m = /Opera Mini/i, n = /(CriOS|Chrome)(?=.*\bMobile\b)/i, o = /(?=.*\bFirefox\b)(?=.*\bMobile\b)/i, p = new RegExp("(?:Nexus 7|BNTV250|Kindle Fire|Silk|GT-P1000)", "i"), q = function (a, b) { return a.test(b) }, r = function (a) { var r = a || navigator.userAgent, s = r.split("[FBAN"); return "undefined" != typeof s[1] && (r = s[0]), s = r.split("Twitter"), "undefined" != typeof s[1] && (r = s[0]), this.apple = { phone: q(b, r), ipod: q(c, r), tablet: !q(b, r) && q(d, r), device: q(b, r) || q(c, r) || q(d, r) }, this.amazon = { phone: q(g, r), tablet: !q(g, r) && q(h, r), device: q(g, r) || q(h, r) }, this.android = { phone: q(g, r) || q(e, r), tablet: !q(g, r) && !q(e, r) && (q(h, r) || q(f, r)), device: q(g, r) || q(h, r) || q(e, r) || q(f, r) }, this.windows = { phone: q(i, r), tablet: q(j, r), device: q(i, r) || q(j, r) }, this.other = { blackberry: q(k, r), blackberry10: q(l, r), opera: q(m, r), firefox: q(o, r), chrome: q(n, r), device: q(k, r) || q(l, r) || q(m, r) || q(o, r) || q(n, r) }, this.seven_inch = q(p, r), this.any = this.apple.device || this.android.device || this.windows.device || this.other.device || this.seven_inch, this.phone = this.apple.phone || this.android.phone || this.windows.phone, this.tablet = this.apple.tablet || this.android.tablet || this.windows.tablet, "undefined" == typeof window ? this : void 0 }, s = function () { var a = new r; return a.Class = r, a }; "undefined" != typeof module && module.exports && "undefined" == typeof window ? module.exports = r : "undefined" != typeof module && module.exports && "undefined" != typeof window ? module.exports = s() : "function" == typeof define && define.amd ? define("isMobile", [], a.isMobile = s()) : a.isMobile = s() }(this);

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
                if (element)
                    element.innerHTML += defaultText;
            });
    };

    $(function () {
        // Display the download section only for Desktops
        $(isMobile.any ? ".onlyDesktop" : ".onlyMobile").hide();

        // Gets the latest news feed
        getFeed("https://raw.githubusercontent.com/JimiC/jimic.github.io/master/mds/latestNews.md",
            $(".latest-news")[0],
            "Unable to get the latest news.");

        // Gets the change log feed
        getFeed("https://raw.githubusercontent.com/JimiC/jimic.github.io/master/mds/changeLog.md",
            $(".change-log")[0],
            "Unable to get the change logs.");

        // Gets the latest semantic version
        (function () {
            var versionText = "Unknown";
            $.get("https://bitbucket.org/EVEMonDevTeam/evemon/wiki/updates/patch.xml")
                .done(function (data) {
                    try {
                        var xmlDoc = $.parseXML(data);
                        versionText = $(xmlDoc).find("version").text();
                        versionText = versionText.substr(0, versionText.lastIndexOf("."));

                        if (!isMobile.any) {
                            var installerLink = $(xmlDoc).find("autopatchurl").text();
                            if ($("#installer")[0])
                                $("#installer")[0].href = installerLink;

                            if ($("#binaries")[0])
                                $("#binaries")[0].href = installerLink.replace("install", "binaries").replace("exe", "zip");
                        }
                    } catch (e) {
                        console.log(e);
                    }
                })
                .fail(function (e) {
                    console.log(e);
                })
                .always(function () {
                    if ($("#version"))
                        $("#version").text(versionText);
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
}(jQuery))
