

## Welcome

Welcome you to TodoTimeManager project.
This project is to manage my time via using Todo items with priority(Important) and state(Todo, Doing, Done).

## Summary
1. Domain Object: TodoItem, User.
2. ER: TodoItem(Id, Name, Priority, State, Deadline), User(Id, Username, Password).
3. Design the restful APIs:.

API | Description | Request | Response
--- | --- | --- | ---
POST /api/login | log in | Json(username, password) | Json(accessToken(30mins), User)
Delete /api/login | sign out | Header:Authorization={accessToken} | None
GET /api/todoitems | Get all todo items | accessToken | List of todo item
GET /api/todoitems/{id} | Get an item via Id | accessToken | todo item
POST /api/todoitems | Add a new todo item | accessToken and todo item | todo item
PUT /api/todoitems/{id}/{state} | change an existing todo-item's state | accessToken, id and state | Boolean
PUT /api/todoitems/{id} | update an existing todo-item | accessToken and todo item | Boolean


## Timesheet
1. 2019-10-24: Analyze file "RUSH.DotNetCore.CompetencyTest.pdf" and make a plan and todo items.
2. 2019-10-25: Read some of Azure, K8s, Terraform, Helm and so on.
3. 2019-10-26: Construct the environments of these infrastructures of Azure, K8s, SQL Database, Redis, Load Balance, Storage, Terraform and Helm. And Write simple demo of asp.net core to realize CI via using these infrastructures.
4. 2019-10-27: Construct todo web application. I want to structure a todo items of my time. Coding, test, debug and deploy, etc.
5. 2019-10-28: Add distributed cached Redis to project to realize login model. And use Event Sourcing pattern to persistence todo items.
6. 2019-10-28: Writing summary file README.md.


## Infrastructure

Credentials: 

Subscription ID: 4504fe8d-e339-4d64-b155-1737d124f5ce
Directory ID: 554c93c7-6dcf-4f50-bad3-a847ffa70b98
Application ID: c64af45c-60f4-478a-9861-c47af3c6d609
Application Secret Key
