#pragma once

#ifndef _UTILITIES_h
#define _UTILITIES_h

#include <Arduino.h>

#ifdef __arm__
// should use uinstd.h to define sbrk but Due causes a conflict
extern "C" char* sbrk(int incr);
#else  // __ARM__
extern char* __brkval;
#endif  // __arm__

int freeMemory() {
    char top;
#ifdef __arm__
    return &top - reinterpret_cast<char*>(sbrk(0));
#elif defined(CORE_TEENSY) || (ARDUINO > 103 && ARDUINO != 151)
    return &top - __brkval;
#else  // __arm__
    return __brkval ? &top - __brkval : &top - __malloc_heap_start;
#endif  // __arm__
}

struct MemoryTest
{
public:
    int start = 0;
    int alloc = 0;
    int del = 0;
    void Start() { start = freeMemory(); }
    void Alloc() { alloc = freeMemory(); }
    void End() { del = freeMemory(); }
    void Display(const char* name)
    {
        Serial.println(name);
        Serial.println("Summary:");
        Serial.print("Total object memory: ");
        Serial.println(start - alloc);
        if (start > del)
        {
            Serial.println(" [!] Warning! Memory leak!");
            Serial.print(start - del);
            Serial.print("/");
            Serial.print(start - alloc);
            Serial.println(" bytes leaked");
        }
        if (start < del)
        {
            Serial.println(" [!] Warning! Memory loss!");
            Serial.print(del - start);
            Serial.println(" bytes deleted");
        }
    }
};

#define START_MONITOR_MEMORYTEST(TestName)\
MemoryTest TestName = MemoryTest();\
TestName.Start();

#define ALLOC_MONITOR_MEMORYTEST(TestName)\
TestName.Alloc();

#define END_MONITOR_MEMORYTEST(TestName)\
TestName.End();\
TestName.Display(#TestName);

#define START_ADV_MEMORYTEST(name, classType, valueName, ...)\
if (true)\
{\
    Serial.println(#name);\
    int __start = freeMemory();\
    Serial.print("Start memory usage: ");\
    Serial.println(__start);\
    classType* valueName = new classType(__VA_ARGS__);

#define ALLOC_ADV_MEMORYTEST()\
    int __alloc = freeMemory();\
    Serial.print("Allocated memory usage: ");\
    Serial.println(__alloc);

#define END_ADV_MEMORYTEST(valueName)\
    delete(valueName);\
    int __del = freeMemory();\
    Serial.print("Deleted memory usage: ");\
    Serial.println(__del);\
    Serial.println("Summary:");\
    Serial.print("Total object memory: ");\
    Serial.println(__start - __alloc);\
    if (__start > __del)\
    {\
        Serial.println(" [1] Warning! Memory leak!");\
        Serial.print(__start - __del);\
        Serial.print("/");\
        Serial.print(__alloc - __start);\
        Serial.println(" bytes leaked");\
    }\
    if (__start < __del)\
    {\
        Serial.println(" [!] Warning! Memory loss!");\
        Serial.print(__del - __start);\
        Serial.println(" bytes deleted");\
    }\
}

#define MEMORYTEST(name, classType, ...)\
if (true)\
{\
    Serial.println(#name);\
    int start = freeMemory();\
    Serial.print("Start memory usage: ");\
    Serial.println(start);\
    classType* __VALUE__ = new classType(__VA_ARGS__);\
    int alloc = freeMemory();\
    Serial.print("Allocated memory usage: ");\
    Serial.println(alloc);\
    delete(__VALUE__);\
    int del = freeMemory();\
    Serial.print("Deleted memory usage: ");\
    Serial.println(del);\
    Serial.println("Summary:");\
    Serial.print("Total object memory: ");\
    Serial.println(start - alloc);\
    if (start > del)\
    {\
        Serial.println(" [!] Warning! Memory leak!");\
        Serial.print(start - del);\
        Serial.print("/");\
        Serial.print(alloc - start);\
        Serial.println(" bytes leaked");\
    }\
    if (start < del)\
    {\
        Serial.println(" [!] Warning! Memory loss!");\
        Serial.print(del - start);\
        Serial.println(" bytes deleted");\
    }\
}

void TestMemory(bool newCycle = false)
{
    static int starttotalmem = freeMemory();
    static int startcyclemem = starttotalmem;
    static int startquerrymem = starttotalmem;
    static int lastcyclemem = -1;
    static int lastquerrymem = 0;

    static int cycle = 0;
    static int querry = 0;

    int currentmem = freeMemory();

    lastquerrymem = startquerrymem;
    startquerrymem = currentmem;

    if (newCycle)
    {
        querry = 0;
        cycle++;
        lastcyclemem = startcyclemem;
        startcyclemem = currentmem;
        Serial.println(cycle);
    }
    querry++;

    Serial.print("-");
    Serial.println(querry);
    
    Serial.print("\tTotal memory delta: ");
    Serial.println(currentmem - starttotalmem);

    if (newCycle)
    {
        Serial.print("Total memory: ");
        Serial.println(currentmem);
        Serial.print("\tCycle memory delta: ");
        Serial.println(startcyclemem - lastcyclemem);
    }

    Serial.print("\tQuerry memory delta: ");
    Serial.println(startquerrymem - lastquerrymem);
}

#endif