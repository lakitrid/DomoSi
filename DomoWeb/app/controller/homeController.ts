module App.Controllers {
    "use strict";

    export class HomeController {
        static $inject = ["$scope", "$cookieStore"];

        constructor(private $scope: Scope.IHomeScope, private $cookieStore : angular.cookies.ICookieStoreService) {
            if (angular.isUndefined(this.$scope.home)) {
                this.$scope.home = new Scope.HomeScope($cookieStore);
            }

            /**
             * Sidebar Toggle & Cookie Control
             */
            var mobileView = 992;

            $scope.$watch($scope.home.getWidth, function (newValue, oldValue) {
                if (newValue >= mobileView) {
                    if (angular.isDefined($cookieStore.get('toggle'))) {
                        $scope.home.toggle = !$cookieStore.get('toggle') ? false : true;
                    } else {
                        $scope.home.toggle = true;
                    }
                } else {
                    $scope.home.toggle = false;
                }

            });

            window.onresize = function () {
                $scope.$apply();
            };
        }
    }
} 