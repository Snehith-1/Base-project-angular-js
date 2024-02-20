/**=========================================================
 * Module: access-login.js
 * Demo for login api
 =========================================================*/

(function () {
    'use strict';

    angular
        .module('app.pages')
        .controller('LoginFormController', LoginFormController);

    LoginFormController.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];
    function LoginFormController($rootScope, $scope, $state, $cookies, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        var vm = this;

        activate();

        ////////////////

        function activate() {
            localStorage.clear();
            $cookies.remove('token');
            $scope.isemailexist = false;
            var urlapi = 'api/Login/GetOTPFlag';
            lockUI();
            SocketService.get(urlapi).then(function (resp) {
                unlockUI();
                if (resp.data.otp_flag == "Y" || resp.data.otp_flag == "y") {
                    $scope.showotp = true;
                }
                else if (resp.data.otp_flag == "N" || resp.data.otp_flag == "n") {
                    $scope.showotp = false;
                }
            })

            if ($scope.otpswitch = true) {
                if (window.location.hostname == "localhost") {
                    $scope.bg_image = "url('app/img/vc4.jpg')";
                    $scope.company_logo = "app/img/storyboarderp logo.png";
                }
                else if (window.location.hostname == "erp.vcidex.com") {
                    $scope.bg_image = "url('app/img/vc4.jpg')";
                    $scope.company_logo = "app/img/storyboarderp logo.png";
                }
                else if (window.location.hostname == "tools.samunnati.com") {
                    $scope.bg_image = "url('app/img/bg-23.jpg')";
                }
                else if (window.location.hostname == "mentis.storyboarderp.com") {
                    $scope.bg_image = "url('app/img/vc4.jpg')";
                    $scope.company_logo = "app/img/storyboarderp logo.png";
                }
                else if (window.location.hostname == "vcm.storyboarderp.com") {
                    $scope.bg_image = "url('app/img/vc4.jpg')";
                    $scope.company_logo = "app/img/storyboarderp logo.png";
                }
                else if (window.location.hostname == "bethany.storyboarderp.com") {
                    $scope.bg_image = "url('app/img/vc4.jpg')";
                }
                else {
                    $scope.bg_image = "url('app/img/bg-23.jpg')";
                    $scope.company_logo = "app/img/storyboarderp logo.png";
                }


                // bind here all data from the form
                vm.account = {};
                // place the message if something goes wrong
                vm.authMsg = '';

                vm.login = function () {
                    vm.authMsg = '';


                    if (vm.loginForm.$valid) {

                        //$http
                        //  .post('api/Login/login', {email: vm.account.email, password: vm.account.password})
                        //  .then(function(response) {
                        //    // assumes if ok, response is an object with some data, if not, a string with error
                        //    // customize according to your api
                        //    if ( !response.account ) {
                        //      vm.authMsg = 'Incorrect credentials.';
                        //    }else{
                        //      $state.go('app.dashboard');
                        //    }
                        //  }, function() {
                        //    vm.authMsg = 'Server Request Error';
                        //  });

                        var params = {
                            user_code: $scope.login.account.name,
                            user_password: $scope.login.account.password
                        }
                        var url = apiManage.apiList['login'].api;
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.user_gid != "") {
                                $cookies.putObject('token', resp.data.token);
                                localStorage.setItem('user_gid', resp.data.user_gid);
                                if (window.location.hostname == "erp.vcidex.com") {
                                    $state.go('app.hrmDashboard');
                                }
                                else if (window.location.hostname == "mentis.storyboarderp.com") {
                                    $state.go('app.hrmDashboard');
                                }
                                else if (window.location.hostname == "test.mentis.storyboarderp.com") {
                                    $state.go('app.hrmDashboard');
                                }

                                else if (window.location.hostname == "test.vcidex.storyboarderp.com") {
                                    $state.go('app.hrmDashboard');
                                }
                                else if (window.location.hostname == "test.samunnati.storyboarderp.com") {
                                    $state.go('app.assetDashboard');
                                }
                                else if (window.location.hostname == "uat.vcidex.storyboarderp.com") {
                                    $state.go('app.hrmDashboard');
                                }
                                else if (window.location.hostname == "uat.mentis.storyboarderp.com") {
                                    $state.go('app.hrmDashboard');
                                }
                                else if (window.location.hostname == "test.mentis.com") {
                                    $state.go('app.hrmDashboard');
                                }

                                else {
                                    $state.go('app.CommonMstDashboard');
                                }
                                unlockUI();
                            }
                            else if (resp.data.message == "Deactivate") {
                                unlockUI();
                                vm.authMsg = 'User Deactivated. kindly contact your Admin.';
                            }
                            else if (((resp.data.user_gid == "") || (resp.data.user_gid == null)) && ((resp.data.employee_status == "") || (resp.data.employee_status == null))) {
                                unlockUI();
                                vm.authMsg = 'Invalid Credentials';
                            }
                            else if ((resp.data.user_gid == "") || (resp.data.user_gid == null) || (resp.data.employee_status == "P")) {
                                unlockUI();
                                vm.authMsg = 'The Employee is in Pending Status. Kindly Activate the Employee.';
                            }
                            else {
                                unlockUI();
                                vm.authMsg = 'Error Occured';
                            }
                        });

                    }
                    else {
                        // set as dirty if the user click directly to login so we show the validation messages
                        /*jshint -W106*/
                        vm.loginForm.user_name.$dirty = true;
                        vm.loginForm.account_password.$dirty = true;
                    }
                };



                var urlapi = 'api/Login/GetOTPFlag';

                lockUI();
                SocketService.get(urlapi).then(function (resp) {
                    unlockUI();
                    $scope.otpFlag = resp.data.otp_flag;

                });

            }



        }

        $scope.adclick = function () {

            var url = '';

            if ($scope.otpFlag == 'N') {
                url = "https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/v2.0/authorize?client_id=76209ddc-b037-4ee2-865b-639b13765f71&response_type=code&redirect_uri=http://localhost/v1/response.html&response_mode=query&scope=https://graph.microsoft.com/User.Read&state=12345"

            }
            else if ($scope.otpFlag == 'Y') {
                url = "http://smstest.vcidex.com/v1/#/page/otplogin"
            }
            else {
                url = "https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/v2.0/authorize?client_id=76209ddc-b037-4ee2-865b-639b13765f71&response_type=code&redirect_uri=http://localhost/v1/response.html&response_mode=query&scope=https://graph.microsoft.com/User.Read&state=12345"

            }

            window.location = url;
        }

        function startTimer(duration, display) {
            var timer = duration, minutes, seconds;
            setInterval(function () {
                minutes = parseInt(timer / 60, 10)
                seconds = parseInt(timer % 60, 10);

                // minutes = minutes < 10 ? "0" + minutes : minutes;
                seconds = seconds < 10 ? "0" + seconds : seconds;

                display.textContent = seconds;

                if (--timer < 0) {
                    timer = duration;
                }
            }, 1000);
            setTimeout(function () { document.getElementById("resend_otp").disabled = false; }, 60000);

        }

        function resend_otp_timer() {
            var fiveMinutes = 60 * 1,
                display = document.querySelector('#time');
            startTimer(fiveMinutes, display);
        };

        function otptimeout() {
            window.setTimeout(function () {
                document.getElementById("resend_otp").disabled = false;
            }, 5000);
        }

        $scope.otplogin = function () {
            var url = apiManage.apiList['otplogin'].api;
            var params = {
                employee_emailid: $scope.mailid
            }
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.message == 'Invalid email id') {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000

                    });
                    $scope.mailid = '';
                    return;
                }
                $scope.isemailexist = true;
                $scope.isemailmessage = resp.data.message;


                $scope.otpswitch = false;
                document.getElementById("resend_otp").disabled = true;
                setTimeout(function () { document.getElementById("resend_otp").disabled = false; }, 60000);
                resend_otp_timer();
                //otptimeout();
            });

        }

        $scope.verify_otp = function () {
            if ($scope.password != null && $scope.password != "" && $scope.password != undefined) {
                var url = 'api/Login/otpverify';
                var params = {
                    otpvalue: $scope.password
                }
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_gid', resp.data.user_gid);
                    $state.go('app.welcome');
                });
                //$scope.otpswitch=true;
            }
            else {
                Notify.alert("Kindly enter the OTP!", {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });

            }
        }

        $scope.adclick2 = function () {
            var modalInstance = $modal.open({
                templateUrl: '/OtpLogin.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.otpflag = false;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.Otplogin = function () {

                    var params = {
                        employee_emailid: $scope.login.account.employee_emailid

                    }
                    var url = apiManage.apiList['otplogin'].api;
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.templateUrl("/Otpverify.html");
                            otpverify();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                    //otpverify();


                }
            }

            // $scope.otplogin = function(){
            //     $scope.otpswitch=false;
            //     var params = {
            //         employee_emailid: $scope.mailid
            //     }
            //     var url = apiManage.apiList['OTPlogin'].api;
            //     lockUI();
            //     SocketService.post(url, params).then(function (resp) {

            //         unlockUI();
            //         Notify.alert(resp.data.message, {
            //             status: 'success',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //     });
            // }
            function startTimer() {
                var presentTime = document.getElementById('timer').innerHTML;
                var timeArray = presentTime.split(/[:]+/);
                var m = timeArray[0];
                var s = checkSecond((timeArray[1] - 1));
                if (s == 59) { m = m - 1 }
                if (m < 0) {
                    return
                }

                document.getElementById('timer').innerHTML =
                  m + ":" + s;
                console.log(m)
                setTimeout(startTimer, 1000);

            }

            function checkSecond(sec) {
                if (sec < 10 && sec >= 0) { sec = "0" + sec }; // add zero in front of numbers < 10
                if (sec < 0) { sec = "59" };
                return sec;
            }
            myApp.service('modalProvider', ['$modal', function ($modal) {

                this.openPopupModal = function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/orderCancellationBox.html',
                        controller: 'ModalInstanceCtrl'
                    });
                };
            }]);

            $scope.otpverify = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/OtpLogin.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    $scope.otpflag = true;

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.otpverify = function () {

                        var params = {
                            employee_emailid: $scope.login.account.employee_emailid

                        }
                        var url = apiManage.apiList['otpverify'].api;
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();

                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();
                            }
                        });

                    }


                }

            }
        }

    }
})();