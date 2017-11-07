using System;
using System.Runtime.InteropServices;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Document;

namespace LiveScoreEs
{
    public class RavenDbConfig
    {
        //public const String MyDefaultIndex = "StateChangeEvent/ByTimeStamp";
        private static DocumentStore _instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("IDocumentStore not initialized.");
                return _instance;
            }
        }

        public static IDocumentStore Initialize()
        {
			_instance = new DocumentStore() {
				Url = "http://localhost:9666",
				DefaultDatabase = "LiveScore"
			};
	        //_instance.Configuration.Storage.Voron.AllowOn32Bits = true;
            //_instance.Conventions.DefaultQueryingConsistency = ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite; 
			
            _instance.Initialize();

            //_instance.DatabaseCommands.PutIndex(MyDefaultIndex,
            //                            new IndexDefinitionBuilder<StateChangeEvent>
            //                            {
            //                                Map = matchEvents => from mev in matchEvents select new { mev.Timestamp }
            //                            },
            //                            true /* override */);

            return _instance;
        }
    }
}