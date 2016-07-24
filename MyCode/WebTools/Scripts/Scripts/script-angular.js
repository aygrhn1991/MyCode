var app = angular.module('app', ['ngRoute']);
app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/Page1', { templateUrl: '/Angular/Page1', controller: 'Page1Ctrl' })
        .when('/Page2', { templateUrl: '/Angular/Page2', controller: 'Page2Ctrl' })
        .when('/Page3', { templateUrl: '/Angular/Page3', controller: 'Page3Ctrl' })
        .when('/Page4', { templateUrl: '/Angular/Page4', controller: 'Page4Ctrl' })
        .otherwise({ redirectTo: '/Page1' });
}]);
app.run(function ($http, $rootScope) { });
app.controller('Page1Ctrl', function ($scope, $http, $routeParams, $rootScope, $location) { });
app.controller('Page2Ctrl', function ($scope, $http, $routeParams, $rootScope, $location) { });
app.controller('Page3Ctrl', function ($scope, $http, $routeParams, $rootScope, $location) { });
app.controller('Page4Ctrl', function ($scope, $http, $routeParams, $rootScope, $location) { });