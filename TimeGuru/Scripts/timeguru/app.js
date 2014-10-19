(function (angular) {
    angular
        .module('timeAdmin', ['ngRoute'])
        .config([
            '$routeProvider',
            function ($routeProvider) {
                $routeProvider.
                    when('/project', {
                        templateUrl: 'project.view',
                        controller: 'ProjectController'
                    }).
                    when('/task', {
                        templateUrl: 'task.view',
                        controller: 'TaskController'
                    }).
                    when('/userProejects', {
                        templateUrl: 'userProjects.view',
                        controller: 'UserProjectsController'
                    }).
                    otherwise({
                        redirectTo: '/Admin'
                    });
            }
        ]);
}(window.angular));
