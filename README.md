# FireManager Api
FireManager Api Library

Library is written in C# and used in Asp.Net Core 2.2 applications. This library will query Aladtec's FireManager database based on your credentials supplied by Aladtec. Retrieved XML data is deserialized into objects that can be further manipulated for the clients use.

Schedules:
Schedules that are defined in FireManager's database.

Positions:
Positions that are defined in FireManager's database

Members:
Members that are stored in FireManagers database.

Staffed Positions:
Staffed positions are composed of a Schedule, Position, and Member object.

Testing:
By setting the RunTests parameter to true, you will run the test suite evertime the application starts up. If you wish to run tests manually then set the parameter to false, inject the interface 
```c#
ITestRequests
```
into a controller and call one of the interface methods:

```c#
Task<IList<FireManagerMember>> TestMemberRequest();

Task<IList<FireManagerSchedule>> TestScheduleRequest();

Task<IList<FireManagerPosition>> TestPositionRequest();

Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(DateTime Date);

Task<IList<FireManagerStaffedPosition>> TestStaffedPositionRequest(int Year, int Month);
```

Each method tests a different portion of the api to ensure that the returned data is what you requested or you can run all tests by calling

```c#
Task<bool> RunTestSuite();
```
method.


```C#
services.AddFireManager(options =>
{
   options.AccountKey("<AccountKey>");
   options.AccountUrl("<AccessUrl>");
   options.AccountId("<AccountId>");
   
}, bool RunTests);

```
