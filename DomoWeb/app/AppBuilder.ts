module App {
    "use strict";
    export class AppBuilder {
        app: ng.IModule;

        constructor(name: string) {
            this.app = angular.module(name, [
                "ngAnimate",
                "ngRoute",
                "ngSanitize",
                "ngResource",
                "ui.bootstrap",
                "ui.router",
                "ngCookies"
            ]);
            this.app.config(["$routeProvider", ($routeProvider: ng.route.IRouteProvider) => {
                $routeProvider
                    .when("/", { controller: "DashboardController", templateUrl: "app/views/dashboard.html" })
                    .otherwise({ redirectTo: "/" });
            }]);
            this.app.controller("HomeController", ["$scope", "$cookieStore", ($scope: Scope.IHomeScope, $cookieStore: angular.cookies.ICookieStoreService) => new App.Controllers.HomeController($scope, $cookieStore)]);
            this.app.controller("DashboardController", ["$scope", ($scope: Scope.IDashboardScope) => new App.Controllers.DashboardController($scope)]);
        }

        public start() {
            $(document).ready(() => {
                angular.bootstrap(document, [this.app.name]);
            });
        }
    }
} 