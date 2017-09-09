/**
 * Define the PMS module
 */
(function () {
    'use strict';
    angular.module('pmsApp', ['ngRoute', 'ngAnimate', 'angular-loading-bar',
                              'angularUtils.directives.dirPagination', 'rzModule'])
    .config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeBar = true;
        cfpLoadingBarProvider.includeSpinner = true;
        //cfpLoadingBarProvider.parentSelector = '#loading-bar-container';
        //cfpLoadingBarProvider.spinnerTemplate = '<div class="ajax-loading-status"></div>';
    }]);
})();