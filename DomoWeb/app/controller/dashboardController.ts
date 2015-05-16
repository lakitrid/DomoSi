module App.Controllers {
    "use strict";

    export class DashboardController {
        static $inject = ["$scope", "$http"];

        constructor(private $scope: Scope.IDashboardScope, private $http: angular.IHttpService) {
            if (angular.isUndefined(this.$scope.dashboard)) {
                this.$scope.dashboard = new Scope.DashboardScope();
            }

            this.$scope.dashboard.refresh = setInterval(
                () => {
                    this.$scope.$apply(() => {
                        this.getIndex();
                    });
                },
                30000,
                0);
            this.$scope.$on("$destroy",() => { clearInterval(this.$scope.dashboard.refresh); });

            this.getIndex();
        }

        getIndex() {
            this.$http.get("api/energy").success((data: Domain.EnergyIndex) => {
                this.$scope.dashboard.peekHourIndex = data.PeekHours;
                this.$scope.dashboard.lowHourIndex = data.LowHours;

            });
        }
    }
} 