DB Setup...
1.	dotnet ef migrations add init (from Migration Mgr cmd line)
2.	run DBMigrationMgr (will create db/apply migrations)
	- or -
	dotnet ef database update (rom Migration Mgr cmd line)