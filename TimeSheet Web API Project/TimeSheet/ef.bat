

dotnet-ef migrations add ClientCreate -c ClientContext  

dotnet-ef migrations add MemberCreate -c MemberContext

dotnet-ef migrations add ProjectCreate -c ProjectContext

dotnet-ef migrations add TimeSheetCreate -c TimeSheetContext

dotnet-ef migrations add CategoryCreate -c CategoryContext


dotnet-ef database update -c TimeSheetContext
