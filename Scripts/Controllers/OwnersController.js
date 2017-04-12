(function () {

    var app = angular.module("plunker", ["ui.bootstrap"]);

    app.controller("MainCtrl", function ($scope, $http) {

        $http({
            method: "GET",
            url: "/api/simpsons/"
        }).then(function mySucces(response) {

            $scope.allCandidates = response.data;
            $scope.totalItems = $scope.allCandidates.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 3;

        }, function myError(response) {
            alert(response.statusText);
        });


        $scope.deleteItem = function (IdOwner) {
            $http({
                method: 'DELETE',
                url: "/api/simpsons/" + IdOwner
            }).then(function mySucces(response) {

                $scope.allCandidates = response.data;
                $scope.totalItems = $scope.allCandidates.length;

                $scope.show();
            }, function myError(response) {
                alert(response.statusText);
            });
        }

        $scope.addItem = function (Owner, answerForm) {
 
            if (answerForm.$valid) {

                console.log('we passed validation');

                $http.post("/api/simpsons/", { 'IdPet': 0, 'NamePet': 'aaa', 'IdOwner': 0, 'NameSimpsons': Owner })
                    .then(function mySucces(response) {

                        $scope.allCandidates = response.data;
                        $scope.totalItems = $scope.allCandidates.length;

                        $scope.show();

                    }, function myError(response) {
                        alert(response.statusText);
                    });
            }
        }

        $scope.show = function () {
            $scope.$watch("currentPage", function () {
                setPagingData($scope.currentPage);
            });
        }


        $scope.show();

        function setPagingData(page) {
            $scope.aCandidates = $scope.allCandidates.slice(
                (page - 1) * $scope.itemsPerPage,
                page * $scope.itemsPerPage
            );
        }
    });


})();