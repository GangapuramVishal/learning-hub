param webAppName string // Generate unique String for web app name
param sku string = 'F1' // The SKU of App Service Plan
// param linuxFxVersion string = 'DOTNETCORE|7.0' // The runtime stack of web app
param dotnetVersion string = 'v8.0'
param location string = 'Central India' // Location for all resources
param appServicePlanName string
param clientId string = '123xyz'
param environment string

// var appSettings = {
//   WEBSITE_ADD_SITENAME_BINDINGS_IN_APPHOST_CONFIG: '1'   
//   WEBSITE_SWAP_WARMUP_PING_PATH: '/ready'
//   WEBSITE_SWAP_WARMUP_PING_STATUSES: '200,204'
//   WEBSITE_WARMUP_PATH: '/warmup'
//   ClientId: clientId
//   Diva__Environment: environment
// }


resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
 
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

// resource AppSettingConfig 'Microsoft.Web/sites/config@2021-01-15' = {
//   name: 'appsettings'
//   properties: appSettings
//   parent: appService
// }
