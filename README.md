# RentalSystem
校园租赁平台Demo, .NET5+SignalR+Efcore+SqlServer+vue 
``` bash
RentalServer # 后端
├── Data # (EF Core DbContext)
├── Model # (MVC Models, EF Core entities)
│   └── Dto # (Data Transfer Object)
├── Controllers # (MVC Controllers)
├── Services # (Service logics)
├── Hubs # (SignalR hubs)
├── Pages # (Common pages)
├── wwwroot
│   └── uploads # (file upload folder)
├── Program.cs # Main Program
├── Startup.cs # (DI & Middlewares)
├── appsettings.json (#Configurations)
│
└── ClientApp # 前端
    ├── package.json # npm project & package dependices
    ├── public # Vue host html page & icon
    └── src
        ├── App.vue
        ├── api # axios requests
        ├── assets # Fonts & global styles
        ├── components # Vue components
        ├── views # Vue pages
        ├── router # Vue router config
        ├── store # Vuex config
        └── main.js # Vue config
```
