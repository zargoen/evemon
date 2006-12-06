/****h* lgLcdClassLibrary
 * NAME
 *   lgLCDInterface
 *
 * SYNOPSIS
 *   A managed class wrapper for lgLCDWapper. This component can be
 *   built as a .NET assembly, and easily re-used in other .NET compatible
 *   languages.
 *
 * DESCRIPTION
 *   This program has a number of files that are additional
 *   to those used in the simple.c example. These are
 *   
 *     lgLcdPinvoke.h: This file contains pinvoke style 
 *     re-definitions for the original lglcd.h API.
 *
 *     lgLcdLibWrapper.dll: This is a thin wrapper DLL that finds
 *     the location of the installed Logitech G-series software,
 *     and forwards the calls to that software. This DLL needs to
 *     be in the same directory as the main EXE file (i.e.
 *     side-by-side sharing).
 *
 *   For VS 2003 .NET you must follow the steps below
 *
 *   PRB: Linker Warnings When You Build Managed Extensions for C++ DLL Projects
 *   http://support.microsoft.com/?id=814472
 *
 * CREATION DATE
 *   10/21/2005
 *
 * MODIFICATION HISTORY
 *   10/21/2005  Aidan  Added pinvoke to read button status  
 *   
 *******
 */

#pragma once

#include <vcclr.h>
#include "lgLcdWrapper.h"

namespace lgLcdClassLibrary
{

	public __gc class LCDInterface
	{

		lgLcdWrapper*		LCD;

	public:

        // Several useful values are defined in lglcg.h, these are
        // re-defined here in a manner more accessible to non C++ .NET languages
    
        // Bitmap sizes (from lglcd.h)
        static const int lglcd_BMP_FORMAT_160x43x1 = LGLCD_BMP_FORMAT_160x43x1;
        static const int lglcd_BMP_WIDTH = LGLCD_BMP_WIDTH;
        static const int lglcd_BMP_HEIGHT = LGLCD_BMP_HEIGHT;

        // Priorities
        static const long lglcd_PRIORITY_IDLE_NO_SHOW = LGLCD_PRIORITY_IDLE_NO_SHOW;
        static const long lglcd_PRIORITY_BACKGROUND = LGLCD_PRIORITY_BACKGROUND;
        static const long lglcd_PRIORITY_NORMAL = LGLCD_PRIORITY_NORMAL;
        static const long lglcd_PRIORITY_ALERT = LGLCD_PRIORITY_ALERT;
        static const long lglcd_SYNC_UPDATE_MASK =  0x80000000;

        // Invalid handle definitions
        static const long lglcd_INVALID_CONNECTION = LGLCD_INVALID_CONNECTION;
        static const long lglcd_INVALID_DEVICE = LGLCD_INVALID_DEVICE;

        // Soft-Button masks
        static const long lglcd_BUTTON_BUTTON0 = LGLCDBUTTON_BUTTON0;
        static const long lglcd_BUTTON_BUTTON1 = LGLCDBUTTON_BUTTON1;
        static const long lglcd_BUTTON_BUTTON2 = LGLCDBUTTON_BUTTON2;
        static const long lglcd_BUTTON_BUTTON3 = LGLCDBUTTON_BUTTON3;


		LCDInterface::LCDInterface()
		{
			LCD = NULL;
		}

		// open LCD interface
		Boolean	Open(String* appFriendlyName, Boolean isAutoStartable)
		{
			if (LCD != NULL) return false; 
			
            LCD = new lgLcdWrapper;
			
            // http://support.microsoft.com/?kbid=311259
            // get a pinned pointer to the string
            const __wchar_t __pin *lpsappFriendlyName = PtrToStringChars(appFriendlyName);

            return LCD->Open(lpsappFriendlyName, (Boolean)isAutoStartable);
		}

		// display a bitmap on LCD (open if not already open)
		Boolean	DisplayBitmap(Byte* samplebitmap, Int64 priority)
		{
			if (LCD == NULL) return false; 
			// create a pinned pointer to unmanaged data type
			unsigned char __pin *pPinned = samplebitmap;
			Boolean returnStatus = LCD->DisplayBitmap(pPinned, priority);
			pPinned = 0;
            return returnStatus;
		}

		// close LCD interface
		Boolean	Close()
		{
			if (LCD == NULL) return false; 
			LCD->Close();
			delete LCD;
			LCD = NULL;
			return true;
		}

        // read soft buttons
		Boolean	ReadSoftButtons(Int64& buttons)
		{
            buttons = 0;
			if (LCD == NULL) return false; 
            DWORD buttonsDWORD;
			LCD->ReadSoftButtons(&buttonsDWORD);
            buttons = (long) buttonsDWORD;
			return true;
		}

	};
}
