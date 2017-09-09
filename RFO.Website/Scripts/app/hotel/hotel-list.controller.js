/**
 * Define the hotel controller to get data for binding with view
 */
(function () {
    'use strict';

    angular
        .module('pmsApp')
        .controller('hotelListController', hotelListController);

    // Inject services using dependency injection
    hotelListController.$inject = ['$scope', '$rootScope', '$filter', 'dataService'];

    /**
     * Hotel list controller
     */
    function hotelListController($scope, $rootScope, $filter, dataService) {
        // Request context
        $scope.apiSelectHotelUrl = function () {
            return $rootScope.webApiAdress + "/Api/Hotel/Select";
        };
        $scope.apiSelectEquipmentUrl = function () {
            return $rootScope.webApiAdress + "/Api/Equipment/Select";
        };
        $scope.hotelImageUrl = function (imageName) {
            return $scope.webApiAdress + "/Api/HotelImage/GetImage" + "?filename=" + imageName;
        }
        // Pagination
        $scope.currentPage = 1;
        $scope.numRecordsPerPage = 12;
        $scope.startRecordIndex = function () {
            return ($scope.currentPage - 1) * $scope.numRecordsPerPage;
        };
        $scope.sortColumnIndex = 0;
        $scope.sortDirection = "asc";
        $scope.customFilters = {};
        $scope.searchKeywordPrediction = "";
        $scope.searchPricePrediction = {
            value: 0,
            options: {
                floor: 0,
                ceil: 5000000,
                step: 100000,
                showSelectionBar: true,
                translate: function (value) {
                    return $filter('number')(value);
                }
            }
        };

        // Response context
        $scope.hotels = [];
        $scope.numTotalHotels = 0;
        $scope.equipments = [];

        /**
         * This function is invoked to execute by the time setting of column index for sorting changed
         */
        $scope.$watch('sortColumnIndex', function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.selectHotels();
            }
        });

        /**
         * This function is invoked to execute by the time setting of number hotels per page changed
         */
        $scope.$watch('numRecordsPerPage', function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.selectHotels();
            }
        });

        /**
         * This function is invoked to execute by the time setting of direction for sorting changed
         */
        $scope.$watch('sortDirection', function (newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.selectHotels();
            }
        });

        /**
         * Handle page changed
         * Refer to this link for more detail:
         * https://github.com/michaelbromley/angularUtils/tree/master/src/directives/pagination
         */
        $scope.onPageChanged = function (newPage) {
            $scope.currentPage = newPage;
            $scope.selectHotels();
            $(window).scrollTop(0);
        };

        /**
         * Handle searching 
         */
        $scope.searchHotel = function () {
            var builSelectedEquipments = function () {
                var selectedEquipments = "";
                angular.forEach($scope.equipments, function (equipmentVal, equipmentKey) {
                    if (equipmentVal.HasSelected) {
                        selectedEquipments += (equipmentVal.EquipmentId + ",");
                    }
                });
                return selectedEquipments;
            };
            $scope.customFilters = {
                HotelName: $scope.searchKeywordPrediction,
                Price: $scope.searchPricePrediction.value,
                EquipmentIds: builSelectedEquipments()
            };
            $scope.selectHotels();
        };

        /**
         * Reset hotel searching 
         */
        $scope.resetHotelSearching = function () {
            $scope.searchKeywordPrediction = "";
            $scope.searchPricePrediction.value = 0;
            angular.forEach($scope.equipments, function (equipmentVal, equipmentKey) {
                equipmentVal.HasSelected = false;
            });
            $scope.customFilters = {};
            $scope.selectHotels();
        };

        /**
         * Select all equipments
         */
        $scope.selectEquipments = function () {
            // Build selection request context, this is matching with which one defined in WebApi
            var requestContext = {
                startRecordIndex: 0,
                numRecordsPerPage: 100000, // Select all
                sortColumnIndex: 0,
                sortDirection: "asc"
            };
            // Call data service to get data according specified criteria
            dataService.select($scope.apiSelectEquipmentUrl(), requestContext)
                .then(
                    function (responseContext) { // Success callback
                        if (responseContext.Result) {
                            $scope.equipments = responseContext.Records;
                        } else { // Failed to select
                            app.logger.error(responseContext.Description);
                        }
                    },
                    function (errResponse) { // Error callback
                        app.logger.error(String.format("Failed to select equipments with error_code={0}, error_description={1}",
                            errResponse.status, errResponse.statusText));
                    }
                );
        };

        /**
         *  Select Hotels with specified criteria
         */
        $scope.selectHotels = function () {
            // Build selection request context, this is matching with which one defined in WebApi
            var requestContext = {
                startRecordIndex: $scope.startRecordIndex(),
                numRecordsPerPage: $scope.numRecordsPerPage,
                sortColumnIndex: $scope.sortColumnIndex,
                sortDirection: $scope.sortDirection,
                customFilters: $scope.customFilters
            };
            // Call data service to get data according specified criteria
            dataService.select($scope.apiSelectHotelUrl(), requestContext)
                .then(
                    function (responseContext) { // Success callback
                        if (responseContext.Result) {
                            $scope.hotels = responseContext.Records;
                            $scope.numTotalHotels = responseContext.NumTotalRecords;
                        } else { // Failed to select
                            app.logger.error(responseContext.Description);
                        }
                    },
                    function (errResponse) { // Error callback
                        app.logger.error(String.format("Failed to select hotels with error_code={0}, error_description={1}",
                            errResponse.status, errResponse.statusText));
                    }
                );
        };

        // Default loading in initialization
        $scope.selectEquipments();
        $scope.selectHotels(); 
    }
})();