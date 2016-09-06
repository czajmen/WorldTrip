(function () {
    "use strict";


    angular.module("app-trips")
    .controller("tripsController",tripsController);

    function tripsController($http) {
         
        var vm = this;
        vm.trips = [];


        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips").then(function (response) {

            angular.copy(response.data, vm.trips);
            vm.isBusy = false;

            //success
        }, function (error) {

            vm.errorMessage = "Nie udało sie pobrać danych z serwera!";

           
        }).finally(function () {
            vm.isBusy = false;
        });

        vm.addTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
            .then(function (response) {

                vm.trips.push(response.data);
                vm.newTrip = {};

            }, function (error) {
                vm.errorMessage = "Nie udało sie zapisać nowej wycieczki!" + error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };

    }

})();