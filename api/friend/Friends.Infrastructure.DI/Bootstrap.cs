using System;

namespace Friends.Infrastructure.DI
{
    public class Bootstrap
    {
        private readonly ParsableNameValueCollection _appSettings;
        private readonly ContainerFactory _factory = ContainerFactory.Instance;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public Bootstrap(ParsableNameValueCollection appSettings, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if ((_appSettings = appSettings) == null)
                throw new ArgumentNullException("appSettings");

            _configuration = configuration;
            _clientFactory = (IHttpClientFactory)serviceProvider.GetService(typeof(IHttpClientFactory));
            _factory.Initialize(serviceProvider);
        }

        public void Initialize()
        {
            _factory.Registrar<ParsableNameValueCollection>(true, _appSettings);
            RegisterInfrastructure();
            RegisterRepositories();
            RegisterValidators();
            RegisterServices();
        }

        private void RegisterInfrastructure()
        {
            _factory.Registrar<SimpleMongoClient>(true, SimpleMongoClient.CreateFromConfig("mongoclient-", _appSettings));
            _factory.Registrar<IDataBaseFacade>(true, new DataBaseFacade(_factory.Criar<SimpleMongoClient>()));
        }
        private void RegisterRepositories()
        {
            _factory.Registrar<ICounterRepository>(true, new CounterMongoRepository(_factory.Criar<IDataBaseFacade>(), CollectionsNames.CounterCollectionName));
            _factory.Registrar<ICodeMessageRepository>(true, new CodeMessageRepository(_factory.Criar<IDataBaseFacade>(), CollectionsNames.CodeMessagesCollectionName));
            _factory.Registrar<ICategoriaRepository>(true, new CategoriaHttpRepository(_clientFactory));
            _factory.Registrar<IOfertaRepository>(true, new OfertaMongoRepository(_factory.Criar<IDataBaseFacade>(), CollectionsNames.OfertaCollectionName, _factory.Criar<ICounterRepository>()));
            _factory.Registrar<IProdutoRepository>(true, new ProdutoHttpRepository(_clientFactory));
            _factory.Registrar<IKitRepository>(true, new KitMongoRepository(_factory.Criar<IDataBaseFacade>(), CollectionsNames.KitCollectionName, _factory.Criar<ICounterRepository>()));

        }
        private void RegisterValidators()
        {
            _factory.Registrar<ISalvarOfertaServiceValidator>(true, new SalvarOfertaServiceValidator(_factory.Criar<ICodeMessageRepository>(), _factory.Criar<IOfertaRepository>()));
            _factory.Registrar<ISalvarKitServiceValidator>(true, new SalvarKitServiceValidator(_factory.Criar<ICodeMessageRepository>(), _factory.Criar<IKitRepository>()));

        }
        private void RegisterServices()
        {
            _factory.Registrar<ICategoriaService>(true, new CategoriaService(_factory.Criar<ICategoriaRepository>()));
            _factory.Registrar<IOfertaService>(true, new OfertaService(_factory.Criar<IOfertaRepository>(), _factory.Criar<IProdutoRepository>(), _factory.Criar<ISalvarOfertaServiceValidator>()));
            _factory.Registrar<IKitService>(true, new KitService(_factory.Criar<IKitRepository>(), _factory.Criar<IProdutoRepository>(), _factory.Criar<ISalvarKitServiceValidator>()));

        }
    }
}
