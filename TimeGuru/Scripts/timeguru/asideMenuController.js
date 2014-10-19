(function (angular) {
    angular
        .module('timeAdmin')
        .controller("AsideMenuController", [
            '$scope', '$location', function ($scope, $location) {
                $scope.$on('$routeChangeSuccess', function (event, current, previous) {
                    $scope.viewingProject = $location.path() == '/' || $location.path() == '/project';
                    $scope.viewingTask = $location.path() == '/task';
                    $scope.viewingUserProject = $location.path() == '/userProject' || $location.path().indexOf('/UserProject/') > -1;
                   });
            }
        ]);
}(window.angular));
