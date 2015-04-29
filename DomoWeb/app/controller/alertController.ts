module App.Controllers {
    "use strict";

    export class AlertController {
        static $inject = ["$scope", ];

        constructor(private $scope: Scope.IAlertScope) {
            if (angular.isUndefined(this.$scope.alert)) {
                this.$scope.alert = new Scope.AlertScope();
            }
        }
    }
} 