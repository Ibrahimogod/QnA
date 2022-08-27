# QnA

# Installation:

Run `docker compose up` at root directory

After Image bulild and run go to <a>http://localhost:8000/swagger/index.html</a>


Use any of below users to login and get `acess_token`:

`
user1: 
   Email: test1@gmail.com
   Password: Test1@QnA
`

`
user2: 
   Email: test2@gmail.com
   Password: Test2@QnA
`

`
user3: 
   Email: test3@gmail.com
   Password: Test3@QnA
`

`
user4: 
   Email: test4@gmail.com
   Password: Test4@QnA
`

Or Create your new account using `Register` endpoing

all get requests don't requeir user to be authenticated.

all post requests require user to be authenticated.

delete functoinality will not work if the answer not belong to the authenticated user

user will not be able to vote his answers.

goto <a>http://localhost:8000/health-monitor</a> to see health status and monitor down service
