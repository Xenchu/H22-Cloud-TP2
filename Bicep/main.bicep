param location string = resourceGroup().location
@secure()
param administratorPassword string

var nomsApps =[
  'CommandesAPI'
  'FavorisAPI'
  'FichiersAPI'
  'UtilisateursAPI'
  'VehiculesAPI'
  'ClientMVC'
]

module Apps 'modules/webapp.bicep' = [for nomApp in nomsApps: {
  name: nomApp
  params: {
    serviceplanName : 'SP-${nomApp}'
    location: location
    webAppName: nomApp
  }
}]

module SQLDatabase 'modules/SQLDatabase.bicep' = {
  name: 'SQLdatabase'
  params: {
    administratorPassword: administratorPassword
    location: location
    nomsApps: nomsApps
  }
}

module StorageAccount 'modules/StorageAccount.bicep' = {
  name: 'StorageAccount'
  params: {
    location: location
  }
}
