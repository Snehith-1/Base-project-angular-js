
'use strict';
angular
    .module('angle').factory('SocketService', ['$rootScope', '$http', '$window', '$state', '$location', '$cookies',
function ($rootScope, $http, $window, $state, $location, $cookies) {
    var lshostname = $location.host();
    var ls_host = 'http://' + lshostname + '/StoryboardAPI/'; 
    var stcFactory = {};

    $http.defaults.headers.common['Authorization'] = $cookies.getObject('token');
    stcFactory.post = function (path, param) {
        return $http.post(ls_host + path, param).then(function (response) {
            return response;
        });
    };

    stcFactory.postlogin = function (path, param) {
        return $http.post(ls_host + path, param).then(function (response) {
            return response;
        });
    };

    stcFactory.postFile = function (path, params) {
        return $http.post(ls_host + path, params, {
            headers: {
                'Content-Type': undefined,
                'Authorization': $cookies.getObject('token')
            },
            transformRequest: angular.identity
        }).success(function (data, status, headers, config) {
            return data;
        }).error(function (resp) {
            return resp;
        });
    };

    //stcFactory.get = function (path, param) {
    //    $window.location.href = ls_host + path + '?' + param;
    //};
    stcFactory.get = function (path, param) {
        return $http.get(ls_host + path, param, {
            headers: {
                'Content-Type': undefined,
                'Authorization': $cookies.getObject('token')
            }
        }).then(function (response) {
            return response;
        });
    };

    stcFactory.getpg = function (path, param) {
        $window.location.href = ls_host + path + '?' + param;
    };

    stcFactory.getfile = function (path, param) {
        return $http.get(path, param).then(function (response) {
            return response;
        });
    };
    stcFactory.preview = function (path, param) {
        return ls_host + path + '?' + param;
        //return $http.get(ls_host + path + '?' + param)
        //.then(function (response) {
        //    return response;
        //});
    };

    //  made changes for API migration 

    stcFactory.getbyid = function (path, param) {
        return $http.get(ls_host + path + '/' + param).then(function (response) {
            return response;
        });
    };

    stcFactory.getparams = function (path, param) {
        var str = jQuery.param(param);
        return $http.get(ls_host + path + '?' + str).then(function (response) {
            return response;
        });
    };

    stcFactory.delete = function (path, param) {
        //return $http.delete(ls_host + path, param, {
        //    headers: {
        //        'Content-Type': 'application/json'
        //    }
        //}).then(function (response) {
        //    return response;
        //});

        return $http({
            method: 'DELETE',
            url: ls_host + path,
            headers: {
                'Content-Type': 'application/json'
            },
            data: param
        }).then(function (response) {
            return response;
        });
    };

    stcFactory.put = function (path, param) {
        // console.log($cookies.getObject('globals').currentUser.authdata);
        return $http.put(ls_host + path, param, {
            headers: {
                //'Access-Control-Allow-Headers': 'authorization,content-type'
                //'Authorization': $cookies.getObject('globals').currentUser.authdata
            }
        }).then(function (response) {
            return response;
        });
    };
    return stcFactory;


    // end changes for API migration
    stcFactory.openWindow = function (path, param) {
        return ls_host + path + '?' + param;
    };
    return stcFactory;
}]);