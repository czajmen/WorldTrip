(function () {
    "use strict";

    angular.module("app-trips")
    .controller("tripsEditorController", tripsEditorController);


    function tripsEditorController($routeParams, $http) {

        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};


        $http.get("/api/trips/" + vm.tripName + "/stops")
        .then(function (response) {
            angular.copy(response.data, vm.stops);
            _showMap(vm.stops);
        }, function (err) {
            vm.errorMessage = "Bład podczas wczytywania punktów!";
        }).finally(function () {
            vm.isBusy = false;
        });



        vm.addStop = function(){
            vm.isBusy = true;

            $http.post("/api/trips/" + vm.tripName + "/stops", vm.newStop)
            .then(function (response) {

                vm.stops.push(response.data);
                _showMap(vm.stops);
                vm.newStop = {};

            }, function () {
                vm.errorMessage ="Nie udało się dodać nowego punktu!"
            }).finally(function () {
                vm.isBusy = false;
            });

        }

    }




    function _showMap(stops) {
        if (stops && stops.length > 0) {
            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currenStop: 1,
                initialZoom: 3
            });
        }
    }
})();