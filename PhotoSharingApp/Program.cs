using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

namespace PhotoSharingApp

{
    class Program
    {
        static async Task Main(string[] args)
        {
           var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

           var configuration = builder.Build();
//retrieve the Azure storage account connection string from the config file
            var connectionString = configuration["StorageAccountConnectionString"];
//creating a cloud storage account object taking a connection string and an out parameter to return
// the created object
        if(!CloudStorageAccount.TryParse(connectionString, out CloudStorageAccount storageAccount)){
            Console.WriteLine("Unable to parse connection string");
            return;
        }

        var blobClient = storageAccount.CreateCloudBlobClient();
        
        var blobContainer = blobClient.GetContainerReference("photoblobs");

        bool created = await blobContainer.CreateIfNotExistsAsync();

        Console.WriteLine(created ? "Created the Blob container":"Blob container already exist." );
        }
    }
}
