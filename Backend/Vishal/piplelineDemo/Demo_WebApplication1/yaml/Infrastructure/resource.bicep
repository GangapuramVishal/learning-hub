param webAppName string  // Generate unique String for web app name
param sku string = 'F1' // The SKU of App Service Plan
param dotnetVersion string = 'v8.0' // The runtime stack of web app
param location string   // Location for all resources
param appServicePlanName string

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  properties: {
    reserved: false
  }
  sku: {
    name: sku
  }
  kind: 'app'
}
resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: webAppName  
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      netFrameworkVersion: dotnetVersion
    }
  }
}
