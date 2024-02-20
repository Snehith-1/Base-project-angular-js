(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasMstCourierCompanyController', IdasMstCourierCompanyController);

        IdasMstCourierCompanyController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function IdasMstCourierCompanyController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'IdasMstCourierCompanyController';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/IdasMstCourierCompany/CourierCompanySummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
               
                $scope.couriercompany_list = resp.data.MdlCourierCompany;
                 $scope.total = $scope.couriercompany_list.length;
               
            });
        }
        document.getElementById('pagecount').onkeyup = function () {
            // console.log(document.getElementById('pagecount').value);
            if($scope.pagecount==null){
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#DCDCDC';  
            }
            else{
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#ffa';
            }
        };

        $scope.addCourier = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {



                $scope.Submit = function () {

                    if ($scope.description == undefined)
                    {
                        $scope.description=""
                    }

                    var params = {
                        
                        couriercompany_name: $scope.couriercompany_name,
                        description: $scope.description
                       
                    }
                    console.log(params);
                    lockUI();
                    var url = "api/IdasMstCourierCompany/CourierCompany";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.loadMore = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.edit=function(val,CourierName,Description)
        {
            var modalInstance = $modal.open({
                templateUrl: '/editDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                    $scope.courierNameEdit = CourierName;
                    $scope.descriptionEdit = Description;
                   
             
                $scope.close = function () {
                    $modalInstance.close('closed');
                };

                $scope.Update = function () {
                    lockUI();
                    var params = {
                        couriercompany_name: $scope.courierNameEdit,
                        description: $scope.descriptionEdit,
                        couriercompany_gid: val
                    }
                    var url = "api/IdasMstCourierCompany/UpdateCourierCompany";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }
        $scope.delete = function (val) {
            var params = {
                couriercompany_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Courier Company ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IdasMstCourierCompany/DeleteCourierCompany";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
      
    }
})();
