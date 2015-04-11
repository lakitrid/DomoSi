module App.Controllers {
    "use strict";

    export class DashboardController {
        static $inject = ["$scope", ];

        constructor(private $scope: Scope.IDashboardScope) {
            if (angular.isUndefined(this.$scope.dashboard)) {
                this.$scope.dashboard = new Scope.DashboardScope();
            }
        }
    }
} 