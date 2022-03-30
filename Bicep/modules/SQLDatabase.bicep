param location string = resourceGroup().location

param dbServerName string = 'SQLServer'
param administratorLogin string = 'Admin00'
@secure()
param administratorPassword string

param nomsApps array

resource SQLServer 'Microsoft.Sql/servers@2021-08-01-preview' = {
  name: '${dbServerName}-${uniqueString(resourceGroup().id)}'
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorPassword
  }
}

resource firewallRules 'Microsoft.Sql/servers/firewallRules@2021-08-01-preview' = {
  parent: SQLServer
  name: 'AllowAzureIPs'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '255.255.255.255'
  }
}

resource dataBases 'Microsoft.Sql/servers/databases@2021-08-01-preview' = [for nom in nomsApps: if(nom == 'CommandesAPI' || nom == 'UtilisateursAPI' || nom == 'VehiculesAPI') {
  parent: SQLServer
  name: 'DB-${nom}'
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}]
