(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBuyerViewController', MstBuyerViewController);

    MstBuyerViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function MstBuyerViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBuyerViewController';
        activate();

        function activate() {
            $scope.buyer_gid = localStorage.getItem('buyer_gid');
            var param = {
                buyer_gid: $scope.buyer_gid
            }

            var url = 'api/MstCreditStatusAdd/buyerDetailsEdit';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyer_code = resp.data.buyer_code;
                $scope.buyer_name = resp.data.buyer_name;
                $scope.coi_date = resp.data.editcoi_date;
                $scope.businessstart_date = resp.data.editbusinessstart_date;
                $scope.year_business = resp.data.year_business;
                $scope.month_business = resp.data.month_business;
                $scope.constitution_name = resp.data.constitution_name;
                $scope.cin_no = resp.data.cin_no;
                $scope.pan_no = resp.data.pan_no;
                $scope.contactperson_fn = resp.data.contactperson_firstname;
                $scope.contactperson_mn = resp.data.contactperson_middlename;
                $scope.contactperson_ln = resp.data.contactperson_lastname;

                unlockUI();
            });

            var url = 'api/MstCreditStatusAdd/buyerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });

            var url = 'api/MstCreditStatusAdd/buyerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.buyeraddress_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerBankList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankdetail_list = resp.data.bank_list;
            });


        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.back = function () {
            $state.go('app.MstBuyerSummary');
        }

    }
})();
