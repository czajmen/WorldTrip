(function () {

    "use strict";

    angular.module("app-trips", ["simpleControls", "ngRoute"])
    .config(function ($routeProvider) {

        $routeProvider.when("/", {
            conroller: "tripsController",
            conrollerAs: "vm",
            templateUrl: "/views/tripsView.html"

        });

        $routeProvider.when("/editor/:tripName", {
            controller: "tripsEditorController",
            controllerAs: "vm",
            templateUrl: "/views/tripsEditorView.html"

    });

        $routeProvider.otherwise({reditectTo: "/"});

    });
})();