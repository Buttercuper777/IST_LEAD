# IST LEAD
## Client documentation
![](https://img.shields.io/badge/-Node.Js-6cc24a?logo=nodedotjs&logoColor=white) ![](https://img.shields.io/badge/-Directus-8866ff?logo=directus&logoColor=white) ![](https://img.shields.io/badge/-Next.Js-61DAFB?logo=nextdotjs&logoColor=black) ![](https://img.shields.io/badge/-PostgreSQL-3881C1?logo=postgresql&logoColor=white)

- Clone this repository
- Go to `./lead_client`
- Install packages `npm install`
- Create an env file to configure the application.
Creation path:  `Application/lead_client/.env.local`

### .env file example:
    #GRAPHQL URL's
        GRAPHQL = YOUR GRAPHQL API 
        NEXT_PUBLIC_GRAPHQL = YOUR PUBLIC GRAPHQL API
    
    #Base Directus API
        NEXT_PUBLIC_DIRECTUS_API = YOUR DIRECTUS REST API 
    
    #Local JWT SECRET!
        JWT_SECRET = JWT SECRET STRING
    
    #.NET API
        NEXT_PUBLIC_LEAD_API = APPLICATION API PATH

### Running application:
 - **[Next.js documentation](https://nextjs.org/docs#manual-setup "NextJS documentation")**
 
```javascript
"scripts": {
  "dev": "next dev",
  "build": "next build",
  "start": "next start",
  "lint": "next lint"
}
```
------------
## Server documentation
![](https://img.shields.io/badge/-.NET-512bd4?logo=dotnet&logoColor=white) ![](https://img.shields.io/badge/-Cloudinary-4573D5?logo=&logoColor=white) ![](https://img.shields.io/badge/-PostgreSQL-3881C1?logo=postgresql&logoColor=white)

- Go to `Application/IST_LEAD/IST_LEAD.LEAD_API`
- Create an env file to configure the application.
Creation path:  `Application/IST_LEAD/IST_LEAD.LEAD_API/.env`

### .env file example:
    #Cloudinary Account
        CloudName='NAME'
        ApiKey='API KEY'
        ApiSecret='SECRET'
    
    #PostgeSQL Connection
        PostgreConnectionString = Server=[IP]; Port=5432; Database = [DB NAME]; User Id = [USER NAME]; password = [USER PASS]
    
    #Local Directus Connection
        LocalDirectus=Directus REST API
       
    #DirectusBearerForAPI
        BearerForAPI=BEARER DIRECTUS API USER






