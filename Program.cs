using IoC_Containers;

var container = new Ioc_Container();
container.AddSingleton<IEmailSender, EmailSender>();
container.AddSingleton<ISmsAPI, SmsAPI>();

var authService = container.Resolve<AuthService>();
authService.Register();
authService.Login();

var subscriptionService = container.Resolve<SubscriptionService>();
subscriptionService.Subscribe();

var notificationService = container.Resolve<NotificationService>();
notificationService.FromEmail("Вы поставили лайк");
notificationService.FromSms("Вы поставили лайк");