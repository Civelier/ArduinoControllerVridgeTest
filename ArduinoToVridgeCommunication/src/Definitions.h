#pragma once

#ifndef _DEFINITIONS_h
#define _DEFINITIONS_h

#ifdef _DEBUG
#define DEBUG_LEVEL 1
#else
#define DEBUG_LEVEL 0
#endif

#define Write(text) Serial.print(text)

#define WriteLine(text) Serial.println(text)

#define DebugWrite(text, lvl) if (lvl <= DEBUG_LEVEL) Write(text)

#define DebugWriteLine(text, lvl) if (lvl <= DEBUG_LEVEL) WriteLine(text)

#endif