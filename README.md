# WebAppBlog
Web Application - Blog website - ASP.NET MVC 5, Entity Framework

It is used for writing articles, providing  updates, educating users. This is a clear demonstration of a usual ASP.NET(MVC) application built on .NET Framework
and using EntityFramework(Code first Model) to create a database. Great practice of async operations using TAP(Task-based Asynchronous Pattern).

## Code Examples

### Home Page
- ``Index`` - action method that sends model from database to view
```
public async Task<ActionResult> Index(PageModel model) =>  View(await _dbContext.GetPaginatablePostsAsync(_itemsPerPage, model));
```
### Pagination
```
public ActionResult Pages(PageModel model) => View(_dbContext.GetPages(model));
```
