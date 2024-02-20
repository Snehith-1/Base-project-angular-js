(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditIndividualAPIController', AgrTrnSuprCreditIndividualAPIController);

    AgrTrnSuprCreditIndividualAPIController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnSuprCreditIndividualAPIController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditIndividualAPIController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lsapi_name = $location.search().lsapi_name;
        var lsapi_name = $scope.lsapi_name;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            $scope.lsapi_name = $location.search().lsapi_name;
            var params = {
                function_gid: contact_gid
            }
            var url = 'api/AgrMstSuprAPIVerifications/GetStateList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mstgst_list = resp.data.mstgst_list;
            });
            var url = 'api/AgrMstSuprAPIVerifications/GetIECDetailed';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.IECDetailed_list = resp.data.IECDetailed_list;
            });
            var url = 'api/AgrMstSuprAPIVerifications/GetFDA';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.fda_list = resp.data.fda_list;

            });
            var url = 'api/AgrMstSuprAPIVerifications/GetFSSAI';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.fssai_list = resp.data.fssai_list;

            });
            var url = 'api/AgrMstSuprAPIVerifications/GetLPGIDList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.LPGID_list = resp.data.LPGID_list;
            });
            var url = 'api/AgrMstSuprAPIVerifications/GetShopList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.shop_list = resp.data.shop_list;
            });
            var url = 'api/AgrMstSuprAPIVerifications/GetRCAuthAdvancedList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
            });
            var url = 'api/AgrMstSuprAPIVerifications/GetRCSearchList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RCSearch_list = resp.data.RCSearch_list;
            });
            var url = 'api/AgrMstSuprAPIVerifications/GetPropertyTaxList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.PropertyTax_list = resp.data.PropertyTax_list;
            });
            var paramcrime = {
                contact_gid: contact_gid
            }
            var url = 'api/AgrCrimeCheckAPI/GetCrimeRecordIndividualDetail';
            SocketService.getparams(url, paramcrime).then(function (resp) {
                unlockUI();
                $scope.individual_name = resp.data.individual_name;
                $scope.individual_dob = resp.data.individual_dob;
                $scope.father_name = resp.data.individual_fathername;
                $scope.individualaddress_list = resp.data.individualaddress_list;
            });
            var url = 'api/AgrCrimeCheckAPI/GetIndividualReportList';
            SocketService.getparams(url, paramcrime).then(function (resp) {
                $scope.individualcrimereport_list = resp.data.individualcrimereport_list;
            });


            vm.submitted = false;
            vm.validateInput = function (name, type) {
                var input = vm.formValidate[name];
                return (input.$dirty || vm.submitted) && input.$error[type];
            };

            // Submit form
            vm.submitForm = function () {
                vm.submitted = true;
                if (vm.formValidate.$valid) {
                } else {
                    return false;
                }
            };

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };


            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };



        }


        //IEC Detailed
        $scope.ieconchange = function () {
            $scope.iecverifyvalidation = false;
            $scope.iecstatus = 'notchecked';
            $scope.iecverify_status = '';
        }
        $scope.verifyiec_detailed = function (txtiec_detailed) {
            if (txtiec_detailed == '' || txtiec_detailed == undefined || txtiec_detailed == null) {
                Notify.alert('Kindly Enter Import Export Code', 'warning');
            }
            else {
                var params = {
                    iec_no: txtiec_detailed,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/IECDetailed';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.iecstatus = 'checked';
                    if (resp.data.result.pan != "" && resp.data.result.pan != undefined) {
                        $scope.iecverifyvalidation = true;
                    }
                    else if (resp.data.result.pan == "" || resp.data.result.pan == undefined) {
                        $scope.iecverifyvalidation = false;
                        $scope.iecverify_status = 'notverify';
                        Notify.alert('IEC Details is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addiec_detailed = function () {
            if ($scope.iecstatus == 'checked') {
                var params = {
                    remarks: $scope.txtiec_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostIECDetailed';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetIECDetailed';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.IECDetailed_list = resp.data.IECDetailed_list;

                        });
                        $scope.txtiec_detailed = '';
                        $scope.txtiec_remarks = '';
                        $scope.iecverifyvalidation = false;
                        $scope.iecstatus = '';
                        $scope.iecverify_status = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the IEC Details', 'warning')
            }
        }
        $scope.IECView = function (iecdtl_gid) {
            var iecdtl_gid = iecdtl_gid;
            localStorage.setItem('iecdtl_gid', iecdtl_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstIECDetailedProfileView";
            window.open(URL, '_blank');
        }
        //FSSAI License
        $scope.fssaionchange = function () {
            $scope.fssaiverifyvalidation = false;
            $scope.fssaistatus = 'notchecked';
            $scope.fssaiverify_status = '';
        }
        $scope.verifyfssai = function (txtreg_no) {
            if (txtreg_no == '' || txtreg_no == undefined || txtreg_no == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else {
                var params = {
                    reg_no: txtreg_no,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/FSSAI';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fssaistatus = 'checked';
                    if (resp.data.result.LicType != "" && resp.data.result.LicType != undefined) {
                        $scope.fssaiverifyvalidation = true;
                    }
                    else if (resp.data.result.LicType == "" || resp.data.result.LicType == undefined) {
                        $scope.fssaiverifyvalidation = false;
                        $scope.fssaiverify_status = 'notverify';
                        Notify.alert('FSSAI License is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addfssai = function () {
            if ($scope.fssaistatus == 'checked') {
                var params = {
                    remarks: $scope.txtfssai_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostFSSAI';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetFSSAI';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.fssai_list = resp.data.fssai_list;

                        });
                        $scope.txtreg_no = '';
                        $scope.txtfssai_remarks = '';
                        $scope.fssaiverifyvalidation = false;
                        $scope.fssaistatus = '';
                        $scope.fssaiverify_status = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the FSSAI License', 'warning')
            }
        }

        $scope.FSSAIView = function (fssailicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFSSAIDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fssailicenseauthentication_gid: fssailicenseauthentication_gid
                   }
                var url = 'api/AgrMstSuprAPIVerifications/FSSAIViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtfssai_status = resp.data.fssai_status;
                    $scope.txtlicense_type = resp.data.license_type;
                    $scope.txtlicense_no = resp.data.license_no;
                    $scope.txtfirm_name = resp.data.firm_name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //FDA License
        $scope.fdaonchange = function () {
            $scope.fdaverifyvalidation = false;
            $scope.fdastatus = 'notchecked';
            $scope.fdaverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifyfda = function () {
            if ($scope.txtlicense_no == '' || $scope.txtlicense_no == undefined || $scope.txtlicense_no == null) {
                Notify.alert('Kindly Enter License Number', 'warning');
            }
            else {
                var params = {
                    licence_no: $scope.txtlicense_no,
                    state: $scope.cbostate.state_code,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/fda';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fdastatus = 'checked';
                    if (resp.data.result.store_name != "" && resp.data.result.store_name != undefined) {
                        $scope.fdaverifyvalidation = true;
                    }
                    else if (resp.data.result.store_name == "" || resp.data.result.store_name == undefined) {
                        $scope.fdaverifyvalidation = false;
                        $scope.fdaverify_status = 'notverify';
                        Notify.alert('FDA License Number is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addfda = function () {
            if ($scope.fdastatus == 'checked') {
                var params = {
                    remarks: $scope.txtfda_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostFDA';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetFDA';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.fda_list = resp.data.fda_list;

                        });
                        $scope.txtlicense_no = '';
                        $scope.txtfda_remarks = '';
                        $scope.fdaverifyvalidation = false;
                        $scope.fdastatus = '';
                        $scope.fdaverify_status = '';
                        $scope.cbostate = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the FDA License', 'warning')
            }
        }

        $scope.FDAView = function (fdalicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFDADetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fdalicenseauthentication_gid: fdalicenseauthentication_gid
                   }
                var url = 'api/AgrMstSuprAPIVerifications/FDAViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstore_name = resp.data.store_name;
                    $scope.txtcontact_no = resp.data.contact_no;
                    $scope.txtlicense_detail = resp.data.license_detail;
                    $scope.txtname = resp.data.name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //LPG ID
        $scope.lpgidonchange = function () {
            $scope.lpgidverifyvalidation = false;
            $scope.lpgidstatus = 'notchecked';
            $scope.lpgidverify_status = '';
        }
        $scope.verifylpgid = function (txtlpgid) {
            if (txtlpgid == '' || txtlpgid == undefined || txtlpgid == null) {
                Notify.alert('Kindly Enter LPG ID', 'warning');
            }
            else {
                var params = {
                    lpg_id: txtlpgid,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/LPGIDAuthentication';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lpgidstatus = 'checked';
                    if (resp.data.result.ConsumerName != "" && resp.data.result.ConsumerName != undefined) {
                        $scope.lpgidverifyvalidation = true;
                    }
                    else if (resp.data.result.ConsumerName == "" || resp.data.result.ConsumerName == undefined) {
                        $scope.lpgidverifyvalidation = false;
                        $scope.lpgidverify_status = 'notverify';
                        Notify.alert('LPG ID Details is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addlpgid = function () {
            if ($scope.lpgidstatus == 'checked') {
                var params = {
                    remarks: $scope.txtlpgid_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostLPGID';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetLPGIDList';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.LPGID_list = resp.data.LPGID_list;

                        });
                        $scope.txtlpgid = '';
                        $scope.txtlpgid_remarks = '';
                        $scope.lpgidverifyvalidation = false;
                        $scope.lpgidstatus = '';
                        $scope.lpgidverify_status = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the LPG ID Details', 'warning')
            }
        }

        $scope.LPGIDView = function (lpgiddtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewLPGID.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       lpgiddtl_gid: lpgiddtl_gid
                   }
                var url = 'api/AgrMstSuprAPIVerifications/LPGIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtApproximateSubsidyAvailed = resp.data.result.ApproximateSubsidyAvailed;
                    $scope.txtSubsidizedRefillConsumed = resp.data.result.SubsidizedRefillConsumed;
                    $scope.txtpin = resp.data.result.pin;

                    $scope.txtConsumerEmail = resp.data.result.ConsumerEmail;
                    $scope.txtDistributorCode = resp.data.result.DistributorCode;
                    $scope.txtBankName = resp.data.result.BankName;
                    $scope.txtIFSCCode = resp.data.result.IFSCCode;

                    $scope.txtAadhaarNo = resp.data.result.AadhaarNo;
                    $scope.txtConsumerContact = resp.data.result.ConsumerContact;
                    $scope.txtDistributorAddress = resp.data.result.DistributorAddress;
                    $scope.txtConsumerName = resp.data.result.ConsumerName;

                    $scope.txtConsumerNo = resp.data.result.ConsumerNo;
                    $scope.txtDistributorName = resp.data.result.DistributorName;
                    $scope.txtBankAccountNo = resp.data.result.BankAccountNo;
                    $scope.txtGivenUpSubsidy = resp.data.result.GivenUpSubsidy;

                    $scope.txtConsumerAddress = resp.data.result.ConsumerAddress;
                    $scope.txtLastBookingDate = resp.data.result.LastBookingDate;
                    $scope.txtTotalRefillConsumed = resp.data.result.TotalRefillConsumed;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //SHOP AND ESTABLISHMENT

        $scope.shoponchange = function () {
            $scope.shopverifyvalidation = false;
            $scope.shopstatus = 'notchecked';
            $scope.shopverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifyshop = function () {
            if ($scope.txtregNo == '' || $scope.txtregNo == undefined || $scope.txtregNo == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else if($scope.cbostate == '' || $scope.cbostate == undefined || $scope.cbostate == null){
                Notify.alert('Kindly Select Area Code','warning');
            }
            else {
                var params = {
                    regNo: $scope.txtregNo,
                    areaCode: $scope.cbostate.state_code,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/ShopAndEstablishment';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.shopstatus = 'checked';
                    if (resp.data.statusCode == 101) {
                        $scope.shopverifyvalidation = true;
                    }
                    else if (resp.data.statusCode == 103) {
                        $scope.shopverifyvalidation = false;
                        $scope.shopverify_status = 'notverify';
                        Notify.alert('Shop Registration Number is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addshop = function () {
            if ($scope.shopstatus == 'checked') {
                var params = {
                    remarks: $scope.txtshop_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostShop';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetShopList';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.shop_list = resp.data.shop_list;

                        });
                        $scope.txtregNo = '';
                        $scope.txtshop_remarks = '';
                        $scope.shopverifyvalidation = false;
                        $scope.shopstatus = '';
                        $scope.shopverify_status = '';
                        $scope.cbostate = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the Shop', 'warning')
            }
        }

        $scope.ShopView = function (shopandestablishment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewShopDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       shopandestablishment_gid: shopandestablishment_gid
                   }
                var url = 'api/AgrMstSuprAPIVerifications/ShopViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcategory = resp.data.result.category;
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtcommenceDate = resp.data.result.commenceDate;
                    $scope.txttotalWorkers = resp.data.result.totalWorkers;

                    $scope.txtfatherNameOfOccupier = resp.data.result.fatherNameOfOccupier;
                    $scope.txtemail = resp.data.result.email;
                    $scope.txtwebsiteUrl = resp.data.result.websiteUrl;
                    $scope.txtpdfLink = resp.data.result.pdfLink;

                    $scope.txtownerName = resp.data.result.ownerName;
                    $scope.txtaddress = resp.data.result.address;
                    $scope.txtapplicantName = resp.data.result.applicantName;
                    $scope.txtvalidFrom = resp.data.result.validFrom;

                    $scope.txtnatureOfBusiness = resp.data.result.natureOfBusiness;
                    $scope.txtvalidTo = resp.data.result.validTo;
                    $scope.txtregistrationDate = resp.data.result.registrationDate;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //RC Auth Advanced
        $scope.rcauthadvonchange = function () {
            $scope.rcauthadvverifyvalidation = false;
            $scope.rcauthadvstatus = 'notchecked';
            $scope.rcauthadvverify_status = '';
        }
        $scope.verifyrcauthadv = function (txtregistrationNumber) {
            if (txtregistrationNumber == '' || txtregistrationNumber == undefined || txtregistrationNumber == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else {
                var params = {
                    registrationNumber: txtregistrationNumber,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/VehicleRCAuthAdvanced';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rcauthadvstatus = 'checked';
                    if (resp.data.statusCode == 101) {
                        $scope.rcauthadvverifyvalidation = true;
                    }
                    else if (resp.data.statusCode == 103) {
                        $scope.rcauthadvverifyvalidation = false;
                        $scope.rcauthadvverify_status = 'notverify';
                        Notify.alert('RC Details is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addrcauthadv = function () {
            if ($scope.rcauthadvstatus == 'checked') {
                var params = {
                    remarks: $scope.txtrcauthadv_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostRCAuthAdvanced';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetRCAuthAdvancedList';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;

                        });
                        $scope.txtregistrationNumber = '';
                        $scope.txtrcauthadv_remarks = '';
                        $scope.rcauthadvverifyvalidation = false;
                        $scope.rcauthadvstatus = '';
                        $scope.rcauthadvverify_status = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the RC Details', 'warning')
            }
        }
        $scope.RCAuthAdvancedView = function (vehiclercauthadvanced_gid) {
            var vehiclercauthadvanced_gid = vehiclercauthadvanced_gid;
            localStorage.setItem('vehiclercauthadvanced_gid', vehiclercauthadvanced_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrSuprRCAuthAdvancedView";
            window.open(URL, '_blank');
        }

        //RC Search

        $scope.rcsearchonchange = function () {
            $scope.rcsearchverifyvalidation = false;
            $scope.rcsearchstatus = 'notchecked';
            $scope.rcsearchverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifyrcsearch = function () {
            if ($scope.txtengine_no == '' || $scope.txtengine_no == undefined || $scope.txtengine_no == null) {
                Notify.alert('Kindly Enter Engine Number', 'warning');
            }
            else if ($scope.txtchassis_no == '' || $scope.txtchassis_no == undefined || $scope.txtchassis_no == null) {
                Notify.alert('Kindly Enter Chassis Number', 'warning');
            }
            else if ($scope.cbostate == '' || $scope.cbostate == undefined || $scope.cbostate == null) {
                Notify.alert('Kindly Select State', 'warning');
            }
            else {
                var params = {
                    engine_no: $scope.txtengine_no,
                    chassis_no: $scope.txtchassis_no,
                    state: $scope.cbostate.state_code,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/VehicleRCSearch';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rcsearchstatus = 'checked';
                    if (resp.data.result.rc_regn_no != "" && resp.data.result.rc_regn_no != undefined) {
                        $scope.rcsearchverifyvalidation = true;
                    }
                    else if (resp.data.result.rc_regn_no == "" || resp.data.result.rc_regn_no == undefined) {
                        $scope.rcsearchverifyvalidation = false;
                        $scope.rcsearchverify_status = 'notverify';
                        Notify.alert('Engine Number is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addrcsearch = function () {
            if ($scope.rcsearchstatus == 'checked') {
                var params = {
                    remarks: $scope.txtrcsearch_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostRCSearch';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetRCSearchList';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.RCSearch_list = resp.data.RCSearch_list;

                        });
                        $scope.txtengine_no = '';
                        $scope.txtchassis_no = '';
                        $scope.txtrcsearch_remarks = '';
                        $scope.rcsearchverifyvalidation = false;
                        $scope.rcsearchstatus = '';
                        $scope.rcsearchverify_status = '';
                        $scope.cbostate = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the Engine Number', 'warning')
            }
        }

        $scope.RCSearchView = function (vehiclercsearch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewRCSearchDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       vehiclercsearch_gid: vehiclercsearch_gid
                   }
                var url = 'api/AgrMstSuprAPIVerifications/RCSearchViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrc_manu_month_yr = resp.data.result.rc_manu_month_yr;
                    $scope.txtrc_maker_model = resp.data.result.rc_maker_model;
                    $scope.txtrc_f_name = resp.data.result.rc_f_name;
                    $scope.txtrc_eng_no = resp.data.result.rc_eng_no;

                    $scope.txtrc_owner_name = resp.data.result.rc_owner_name;
                    $scope.txtrc_vh_class_desc = resp.data.result.rc_vh_class_desc;
                    $scope.txtrc_present_address = resp.data.result.rc_present_address;
                    $scope.txtrc_color = resp.data.result.rc_color;

                    $scope.txtrc_regn_no = resp.data.result.rc_regn_no;
                    $scope.txttax_paid_upto = resp.data.result.tax_paid_upto;
                    $scope.txtrc_maker_desc = resp.data.result.rc_maker_desc;
                    $scope.txtrc_chasi_no = resp.data.result.rc_chasi_no;

                    $scope.txtrc_mobile_no = resp.data.result.rc_mobile_no;
                    $scope.txtrc_registered_at = resp.data.result.rc_registered_at;
                    $scope.txtrc_valid_upto = resp.data.result.rc_valid_upto;
                    $scope.txtrc_regn_dt = resp.data.result.rc_regn_dt;

                    $scope.txtrc_financer = resp.data.result.rc_financer;
                    $scope.txtrc_permanent_address = resp.data.result.rc_permanent_address;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //Property Tax

        $scope.propertytaxonchange = function () {
            $scope.propertytaxverifyvalidation = false;
            $scope.propertytaxstatus = 'notchecked';
            $scope.propertytaxverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifypropertytax = function () {
            if ($scope.txtpropertyNo == '' || $scope.txtpropertyNo == undefined || $scope.txtpropertyNo == null) {
                Notify.alert('Kindly Enter Property Number', 'warning');
            }
            else if ($scope.txtcity == '' || $scope.txtcity == undefined || $scope.txtcity == null) {
                Notify.alert('Kindly Enter City', 'warning');
            }
            else if ($scope.cbostate == '' || $scope.cbostate == undefined || $scope.cbostate == null) {
                Notify.alert('Kindly Select State', 'warning');
            }
            else {
                var params = {
                    propertyNo: $scope.txtpropertyNo,
                    city: $scope.txtcity,
                    state: $scope.cbostate.gst_state,
                    district: $scope.txtdistrict,
                    ulb: $scope.txtulb,
                    function_gid: contact_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/PropertyTax';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.propertytaxstatus = 'checked';
                    if (resp.data.statusCode == 101) {
                        $scope.propertytaxverifyvalidation = true;
                    }
                    else if (resp.data.statusCode == 103) {
                        $scope.propertytaxverifyvalidation = false;
                        $scope.propertytaxverify_status = 'notverify';
                        Notify.alert('Property Tax is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addpropertytax = function () {
            if ($scope.propertytaxstatus == 'checked') {
                var params = {
                    remarks: $scope.txtpropertytax_remarks,
                    function_gid: contact_gid,
                }
                lockUI();

                var url = 'api/AgrMstSuprAPIVerifications/PostPropertyTax';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/AgrMstSuprAPIVerifications/GetPropertyTaxList';
                        var params = {
                            function_gid: contact_gid,
                        }
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.PropertyTax_list = resp.data.PropertyTax_list;

                        });
                        $scope.txtpropertyNo = '';
                        $scope.txtcity = '';
                        $scope.txtdistrict = '';
                        $scope.txtulb = '';
                        $scope.txtpropertytax_remarks = '';
                        $scope.propertytaxverifyvalidation = false;
                        $scope.propertytaxstatus = '';
                        $scope.propertytaxverify_status = '';
                        $scope.cbostate = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });

            }
            else {
                Notify.alert('Kindly Verify the Property Tax', 'warning')
            }
        }

        $scope.PropertyTaxView = function (propertytax_gid) {
            var propertytax_gid = propertytax_gid;
            localStorage.setItem('propertytax_gid', propertytax_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/PropertyTaxView";
            window.open(URL, '_blank');
        }



        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/AgrTrnSuprIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/AgrTrnSuprIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/AgrTrnSuprCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/AgrTrnSuprCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/AgrTrnSuprCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/AgrTrnSuprCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_repayment = function () {
            $location.url('app/AgrTrnSuprCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/AgrTrnSuprCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/AgrTrnSuprCreditIndividualBankAcctEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        //AgrSuprKyc API
        $scope.iecdetailed_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
            activate();
        }
        $scope.fssai_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
            activate();
        }
        $scope.fda_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
            activate();
        }
        $scope.lpgid_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
            activate();
        }
        $scope.shop_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
            activate();
        }
        $scope.rcauthadv_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
            activate();
        }
        $scope.rcsearch_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
            activate();
        }
        $scope.propertytax_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
            activate();
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnSuprCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            window.scroll(0, 0);
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
            activate();
        }

        $scope.IECdelete = function (iecdtl_gid) {
            var params =
                {
                    iecdtl_gid: iecdtl_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/IECdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetIECDetailed';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.IECDetailed_list = resp.data.IECDetailed_list;
                });
            });

        }
        $scope.FSSAIdelete = function (fssailicenseauthentication_gid) {
            var params =
                {
                    fssailicenseauthentication_gid: fssailicenseauthentication_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/FSSAIdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetFSSAI';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fssai_list = resp.data.fssai_list;
                });
            });

        }
        $scope.FDAdelete = function (fdalicenseauthentication_gid) {
            var params =
                {
                    fdalicenseauthentication_gid: fdalicenseauthentication_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/FDAdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetFDA';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fda_list = resp.data.fda_list;
                });
            });

        }

        $scope.LPGIDdelete = function (lpgiddtl_gid) {
            var params =
                {
                    lpgiddtl_gid: lpgiddtl_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/LPGIDdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetLPGIDList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.LPGID_list = resp.data.LPGID_list;
                });
            });

        }

        $scope.Shopdelete = function (shopandestablishment_gid) {
            var params =
                {
                    shopandestablishment_gid: shopandestablishment_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/Shopdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetShopList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.shop_list = resp.data.shop_list;
                });
            });

        }

        $scope.RCAuthAdvanceddelete = function (vehiclercauthadvanced_gid) {
            var params =
                {
                    vehiclercauthadvanced_gid: vehiclercauthadvanced_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/RCAuthAdvanceddelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetRCAuthAdvancedList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
                });
            });

        }

        $scope.RCSearchdelete = function (vehiclercsearch_gid) {
            var params =
                {
                    vehiclercsearch_gid: vehiclercsearch_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/RCSearchdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetRCSearchList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.RCSearch_list = resp.data.RCSearch_list;
                });
            });

        }

        $scope.PropertyTaxdelete = function (propertytax_gid) {
            var params =
                {
                    propertytax_gid: propertytax_gid
                }
            var url = 'api/AgrMstSuprAPIVerifications/PropertyTaxdelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {
                    function_gid: contact_gid,
                }
                var url = 'api/AgrMstSuprAPIVerifications/GetPropertyTaxList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.PropertyTax_list = resp.data.PropertyTax_list;
                });
            });

        }



        //CrimeCheck API

        $scope.raise_crimereportrequest = function () {

            if ($scope.cboaddress == '' || $scope.cboaddress == null || $scope.cboaddress == undefined) {
                Notify.alert("Kindly select the address..!", 'warning');
            } else {
                var params = {
                    contact_gid: $scope.contact_gid,
                    application_gid: $scope.application_gid,
                    individual_name: $scope.individual_name,
                    individual_address: $scope.cboaddress.address,
                    individual_dob: $scope.individual_dob,
                    report_mode: 'RealTime'
                }
                lockUI();

                var url = 'api/AgrCrimeCheckAPI/RequestCrimeReportIndividual';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();

                    if (resp.data.status == "OK") {
                        Notify.alert(resp.data.requestStatusMessage, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        individualcrimereport_list();
                    }
                    else {
                        Notify.alert(resp.data.requestStatusMessage, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }




        }


        function individualcrimereport_list() {
            var paramcrime = {
                contact_gid: $scope.contact_gid
            }
            var url = 'api/AgrSuprCrimeCheckAPI/GetIndividualReportList';
            SocketService.getparams(url, paramcrime).then(function (resp) {
                $scope.individualcrimereport_list = resp.data.individualcrimereport_list;
            });
        }

        $scope.crimereport_view = function (crimereportcontact_gid) {
            var crimereportcontact_gid = crimereportcontact_gid;
            localStorage.setItem('crimereportcontact_gid', crimereportcontact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnSuprCrimeReportIndividualView";
            window.open(URL, '_blank');
        }

        $scope.download_crimereport = function (val1, val2) {
            var link = document.createElement("a");
            link.download = val2;
            var uri = val1;
            link.href = uri;
            link.click();
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.individualcrimereport_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualcrimereport_list[i].report_link, $scope.individualcrimereport_list[i].request_id);
            }
        }

    }
})();
