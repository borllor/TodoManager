## Welcome

Welcome you to TodoManager project.
This project is to manage my time via using todo item with state(Todo, Doing, Done).
1. Address: [http://52.163.202.195/](http://52.163.202.195/)
2. Todo Items: [http://52.163.202.195/api/todoitems](http://52.163.202.195/api/todoitems)
3. Please login before visiting todo items({"username":"admin", "password":"123456"}).

## Summary
1. Project "TodoManager" that use Terraform, provision a k8s cluster in Azure.
2. Todo web app implements CRUD and can run in multiple instances.
3. Adopt CQRS and Event Sourcing architecture.
4. "/api/job/todo/checkin" API is used to check-in for the user every day, It is called by Quartz once pre day. It uses event sourcing method to add a check-in-todo-item. 
5. Provision a database inside the AKS cluster.
6. Support sign up / sign in;
7. Some features did not add in because of insufficient time, such as Unit Test, Log and Storing event stream and so on.
9. Design the restful APIs:

API | Description | Request | Response
--- | --- | --- | ---
POST /api/login | log in | Json(username, password) | Json(accessToken(30mins), User)
Delete /api/login | sign out | Header:Authorization={accessToken} | None
GET /api/todoitems | Get all todo items | accessToken | List of todo item
GET /api/todoitems/{id} | Get an item via Id | accessToken | todo item
POST /api/todoitems | Add a new todo item | accessToken and todo item | todo item
PUT /api/todoitems/{id}/{state} | change an existing todo-item's state | accessToken, id and state | Boolean
PUT /api/todoitems/{id} | update an existing todo-item | accessToken and todo item | Boolean
GET /api/job/todo/checkin | Used to call by Quartz pre day  | None | None

## Timesheet
1. 2019-10-24: Analyze file "RUSH.DotNetCore.CompetencyTest.pdf" and make a plan and todo items.
2. 2019-10-25: Read some of Azure, K8s, Terraform, Helm and so on.
3. 2019-10-26: Construct the environments of these infrastructures of Azure, K8s, SQL Database, Redis, Load Balance, Storage, Terraform and Helm. And Write simple demo of asp.net core to realize CI via using these infrastructures.
4. 2019-10-27: Construct todo web application. I want to structure a todo items of my time. Coding, test, debug and deploy, etc.
5. 2019-10-28: Add distributed cached Redis to project to realize login model.
6. 2019-10-28: Writing summary file README.md.
7. 2019-10-28: Recast project to use CQRS and Event Sourcing pattern.


## Infrastructure
Load k8s dashboard.
$ az aks browse --resource-group azure-k8stest --name k8stest

$ docker-compose build

$ docker-compose up


$ docker tag todomanager_web:latest borllor/todo-manager:0.1

$ docker login

$ docker push borllor/todo-manager:0.1


$ helm package todo-manager-mssql

$ helm install todo-manager-mssql

