## Welcome

Welcome you to TodoManager project.
This project is to manage my time via using todo item with state(Todo, Doing, Done).
Please login before visiting "/api/todoitems" API({"username":"admin", "password":"123456"}).

## Development Environment
1. Mac OS
2. Visual studio 2019 for mac and Visual studio code
3. .Net Core 3.0
4. SQL Server
5. Azure cloud

## Summary
1. The project "TodoManager" uses Terraform, provision a k8s cluster in Azure.
2. Todo web app implements CRUD and can run in multiple instances in the k8s cluster.
3. It adopts CQRS and Event Sourcing architecture.
4. "/api/job/todo/checkin" API is used to check-in for the user every day. It is called by Quartz once per day and uses event sourcing method to add a check-in-todo-item. 
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
GET /api/job/todo/checkin | Used to call by Quartz per day  | None | None

## Related instructions
Create aks cluster and access to k8s dashboard.
$ sh ./create-aks-cluster.sh

$ docker-compose build

$ docker-compose up


$ docker tag todomanager_web:latest borllor/todo-manager:0.1

$ docker login

$ docker push borllor/todo-manager:0.1


$ helm package todo-manager-mssql

$ helm install todo-manager-mssql
