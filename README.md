# WebAppBlog
Web Application - Blog website - ASP.NET MVC 5, Entity Framework

It is used for writing articles, providing  updates, educating users. This is a clear demonstration of a usual ASP.NET(MVC) application built on .NET Framework
and using EntityFramework(Code first Model) to create a database. Great practice of async operations using TAP(Task-based Asynchronous Pattern).

## Code Examples

### Home Page
- ``Index`` - action method that sends model from database to the view
- ``PageModel`` - model helper that holds necessary data for the view
- `` async/await`` used because this is an asyncronous request to the database
- ``GetPaginatablePostsAsync`` is an extension method that is held in infrastructure of the application
```sh
public async Task<ActionResult> Index(PageModel model) =>  View(await _dbContext.GetPaginatablePostsAsync(_itemsPerPage, model));
```
### Pagination
```sh
public ActionResult Pages(PageModel model) => View(_dbContext.GetPages(model));
```
### Release History
- 23.04.2021 - work started
- wrok in progress

### Meta
Matin Manafov 
- [@MyFacebook](https://www.facebook.com/matin.manafov.9)
- [@MyLinkedIn](https://www.linkedin.com/in/matin-manafov-69a356176)
- My Gmail:[matin.manafovvvv@gmail.com](matin.manafovvvv@gmail.com)

Distributed under the MIT license. See ``LICENSE`` for more information.

[Repository Link](https://github.com/matin360/WebAppBlog)
