(function () {
	angular
	  .module('pmsApp')
	  .service('dataService', dataService);

    // Inject HTTP service to get data from server
	dataService.$inject = ['$http'];

	/**
     * Define the service to get Hotel data
     */
	function dataService($http) {
		return {
			/**
             *  Select Hotels with specified criteria
             */
			select: function (selectUrl, requestContext) {
				return $http.post(selectUrl, requestContext)
                .then(
                    function (response) { // Success callback 
                    	return response.data;
                    },
                    function (errResponse) { // Error callback
                        return errResponse;
                    }
                );
			},

            /**
             *  Select specified Hotel by Id
             */
			selectById: function (selectByIdUrl, id) {
				return $http.get(selectByIdUrl, id)
                .then(
                    function (response) { // Success callback 
                    	return response.data;
                    },
                    function (errResponse) { // Error callback
                        return errResponse;
                    }
                );
			}
		};
	};
})();