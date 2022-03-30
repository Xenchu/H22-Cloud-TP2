param storageName string = 'stockage'
param location string

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name:'${storageName}${uniqueString(resourceGroup().id)}'
  location:location
  sku:{
    name:'Standard_ZRS'
  }
  kind:'StorageV2'
}

resource imageContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-08-01' = {
  name: '${storageAccount.name}/default/images'
  properties: {
    publicAccess: 'Container'
  }  
}
