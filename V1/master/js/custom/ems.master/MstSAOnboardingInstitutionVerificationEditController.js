
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingInstitutionVerificationEditController', MstSAOnboardingInstitutionVerificationEditController);

    MstSAOnboardingInstitutionVerificationEditController.$inject = ['DownloaddocumentService', '$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstSAOnboardingInstitutionVerificationEditController(DownloaddocumentService, $rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingInstitutionVerificationEditController';

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.sacontactinstitution_gid = searchObject.lssacontactinstitution_gid;
        var sacontactinstitution_gid = $scope.sacontactinstitution_gid;
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open6 = true;
            };
            vm.format = vm.formats[0];
            vm.calender7 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open7 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {

                satype_gid: $scope.cbosatype,
                saentitytype_gid: $scope.cbosaentitytype,

                designation_gid: $scope.cboDesignation,

            }



            var url = 'api/MstSAOnboardingInstitution/GetDropDown';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_salist = resp.data.satype_list;
            });

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_list = resp.data.saentitytype_list;
            });

            SocketService.getparams(url, params).then(function (resp) {
                $scope.designationlist = resp.data.sadesignationlist;
            });


            SocketService.get(url).then(function (resp) {
                $scope.saassessmentagencylist = resp.data.saassessmentagencylist;
            });

            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyratinglist = resp.data.assessmentagencyratinglist;
            });

            var url = 'api/MstSAOnboardingInstitution/TempBureauDocuments';
            SocketService.get(url).then(function (resp) {

            });

            var url = 'api/MstSAOnboardingInstitution/TempSAMailDocument';
            SocketService.get(url).then(function (resp) {

            });

            var url = 'api/MstSAOnboardingInstitution/TempSaVerifyDocument';
            SocketService.get(url).then(function (resp) {

            });


            var url = 'api/AssociateMaster/GetAssociateMasterASC';
            SocketService.get(url).then(function (resp) {
                $scope.associatemaster_list = resp.data.associatemaster_list;
            });

            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });
            var param = {
                sacontactinstitution_gid: sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetInstitutionProspectsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiProspects_list = resp.data.saOnboardInstiProspects_list;
            });

            var url = 'api/MstSAOnboardingInstitution/IndividualList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.onboard_IndividualInsti_list = resp.data.onboard_IndividualInsti_list;
            });

            var url = 'api/MstSAOnboardingInstitution/InstitutionMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiemailaddress_list = resp.data.saOnboardInstiemailaddress_list;
            });
            var url = 'api/MstSAOnboardingInstitution/InstitutionAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardInstiaddress_list = resp.data.saOnboardInstiaddress_list;
            });
            var url = 'api/MstSAOnboardingInstitution/UploadList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
            });

            var url = 'api/MstSAOnboardingInstitution/InstitutionGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gst_Onboard_list = resp.data.gst_Onboard_list;
            });

            var url = 'api/MstSAOnboardingInstitution/GetSABureauInstitutionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactbureau_list = resp.data.sainstitutebureau_list;

            });
            var url = 'api/MstSAOnboardingInstitution/SAMailDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mailuploaddoc_list = resp.data.sauploaddoc_list;
            });
            var url = 'api/MstSAOnboardingInstitution/SaVerifyDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;

            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetBDInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bdinstitutionraisequery_list = resp.data.bdinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetMakerInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.makerinstitutionraisequery_list = resp.data.makerinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetApproverInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approverinstitutionraisequery_list = resp.data.approverinstitutionraisequery_list;
            });
            var url = 'api/MstSAOnboardingInstitution/GetCheckerInstitutionRaiseQuery';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.checkerinstitutionraisequery_list = resp.data.checkerinstitutionraisequery_list;
            });

            var url = 'api/MstSAOnboardingInstitution/InstitutionDetailsEdit';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cbosatype = resp.data.satype_gid;
                $scope.cbosaentitytype = resp.data.saentitytype_gid;
                $scope.txtsamunnati_associate_name = resp.data.sa_associatename;
                $scope.txtsacontact_person_first_name = resp.data.sa_contactfirstname;
                $scope.txtsacontact_person_middle_name = resp.data.sa_contactmiddlename;
                $scope.txtsacontact_person_last_name = resp.data.sa_contactlastname;
                $scope.cboDesignation = resp.data.designation_gid;
                $scope.txtdateofincorporation_date = resp.data.sa_dateofincorporation;
                $scope.txtcompanystart_date = resp.data.sa_companystdate;
                $scope.txtyearin_business = resp.data.sa_yearsinbusiness;
                $scope.txtmonthsin_business = resp.data.sa_monthsinbusiness;
                $scope.txtsa_pannumber = resp.data.sa_companypan;
                $scope.txtannual_turnover = resp.data.sa_annualturnover;
                $scope.txtifsc_code = resp.data.saifsc_code;
                $scope.txtbankaccount_number = resp.data.saaccount_number;
                $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                $scope.txtaccountholder_name = resp.data.saaccountholder_name;
                $scope.txtcancelledcheque_number = resp.data.sacanccheque_number;
                $scope.txtbranch_name = resp.data.sabranch_name;
                $scope.txtbank_name = resp.data.sabank_name;
                $scope.txtcity = resp.data.city;
                $scope.txtdistrict = resp.data.district;
                $scope.txtstate = resp.data.state;
                $scope.txtmicr = resp.data.micr;
                $scope.txtbranch_address = resp.data.branch_address;
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.cbogender = resp.data.gender;
                $scope.cbosaassessmentagency = resp.data.assessmentagency_gid;
                $scope.cboassessmentagencyrating = resp.data.assessmentagencyrating_gid;
                $scope.txtrasteason_date = resp.data.ratingas_datecredit;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.txtutrno = resp.data.utr_no;


                unlockUI();
            });




            var url = 'api/MstSAOnboardingInstitution/ApprovalInitatedDetail';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblrecdsce = resp.data.recordsource;

                $scope.lblsareportingmanager = resp.data.sa_reportingmanager;
                $scope.lblsaupdateddate = resp.data.sa_updated_date;
                $scope.lblsaupdated_by = resp.data.sa_updated_by;
                $scope.rbointerview_evalution_form = resp.data.interviewevalution;
                $scope.rboapplication_form = resp.data.applicationform;
                $scope.rbokycdocuments = resp.data.kycdocuments;
                $scope.rbovettingstatus = resp.data.vettingstatus;
                $scope.rboaddressproof = resp.data.addressproof;
                $scope.rbophotographs = resp.data.photographs;
                $scope.rbocancelledcheckleaf = resp.data.cancelledcheckleaf;
                $scope.txtagreementexecution_date = resp.data.agreementexecutiondate;
                $scope.txtagreementexpiry_date = resp.data.agreementexpirydate;
                $scope.cboagreement_status = resp.data.agreementstatus;
                $scope.txtagroagreementexecution_date = resp.data.agroagreementexecutiondate;
                $scope.txtagroagreementexpiry_date = resp.data.agroagreementexpirydate;
                $scope.cboagroagreement_status = resp.data.agroagreementstatus;
                $scope.txtremarks = resp.data.verificationremarks;
                $scope.approvalinitated_flag = resp.data.approvalinitated_flag;
                $scope.institutionsaveasdraft_flag = resp.data.institutionsaveasdraft_flag;

                $scope.cbosaassessmentagency = resp.data.assessmentagency_gid;
                $scope.cboassessmentagencyrating = resp.data.assessmentagencyrating_gid;
                $scope.txtrasteason_date = resp.data.ratingas_datecredit;


                unlockUI();
            });

        }

        $scope.companydoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.companydoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
       
        $scope.back = function () {
            // $state.go("app.MstSAOnboardingVerificationSummary");

            if (lspage == 'InstitutePending') {
                $location.url('app/MstSAOnboardingVerificationSummary');
            }
            else if (lspage == 'InstituteInitiate') {
                $location.url('app/MstSAOnboardingVerificationSummary');
            }
        }
        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        // $scope.txtbank_address = resp.data.result.address;
                        // $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        // $scope.txtbank_address = '';
                        //$scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }


        $scope.BankAccValidation = function () {
            if ($scope.txtbankaccount_number == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtaccountholder_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtaccountholder_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }


        //Mobile Number Multiple Add maker
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_no == undefined) || ($scope.rdbwhatsapp_no == '') || ($scope.rdbprimary_status == '')) {
                Notify.alert('Enter Mobile No/Select Primary Status and Whatsapp Number', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    samobile_no: $scope.txtmobile_no,
                    saprimary_status: $scope.rdbprimary_status,
                    sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                    sawhatsapp_no: $scope.rdbwhatsapp_no
                }
                var url = 'api/MstSAOnboardingInstitution/MobileNumberAddInEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
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
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/GetTempMobileNoList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;

                    });

                    $scope.txtmobile_no = '';
                    $scope.rdbprimary_status = '';
                    $scope.rdbwhatsapp_no = '';
                    $scope.rdbprimary_no == false;
                });

            }
        }
        $scope.delete_mobileno = function (sainstitution2mobileno_gid) {
            var params =
                {
                    sainstitution2mobileno_gid: sainstitution2mobileno_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteMobileNo';

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
                mobileno_list();
            });
        }
        function mobileno_list() {
            var params =
            {
                sacontactinstitution_gid: $scope.sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetTempMobileNoList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;
            });
        }
        $scope.add_address = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangebusinessstartdate = function () {
                    var params = {
                        businessstart_date: $scope.txtbusinessstart_date
                    }
                    /* var url = 'api/';*/
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtyear_business = resp.data.year_business;
                        $scope.txtmonth_business = resp.data.month_business;
                    });
                }
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/MstSAOnboardingInstitution/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.sacity;
                        $scope.txttaluka = resp.data.sataluka;
                        $scope.txtdistrict = resp.data.sadistrict;
                        $scope.txtstate = resp.data.sastate;
                        $scope.txtcountry = resp.data.sacountry;
                    });

                }

                $scope.getGeoCoding = function () {
                    if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }
                $scope.addressSubmit = function () {
                    var params = {
                        saaddresstype_gid: $scope.cboaddresstype.address_gid,
                        saaddresstype_name: $scope.cboaddresstype.address_type,
                        saprimary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        salandmark: $scope.txtlandmark,
                        sapostal_code: $scope.txtpostal_code,
                        sacity: $scope.txtcity,
                        sataluka: $scope.txttaluka,
                        sadistrict: $scope.txtdistrict,
                        sastate: $scope.txtstate,
                        sacountry: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude

                    }
                    var url = 'api/MstSAOnboardingInstitution/PostAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }
            }
        }
        $scope.delete_address = function (sainstitution2address_gid) {
            var params =
                {
                    sainstitution2address_gid: sainstitution2address_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteAddress';
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
                address_list();
            });
        }
        function address_list() {
            var params =
            {
                sacontactinstitution_gid: $scope.sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetAddressTempList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiaddress_list = resp.data.saOnboardInstiaddress_list;
            });
        }
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, sapostal_code) {
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
                    longitude: longitude,
                    latitude: latitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", sapostal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", sapostal_code.toString());
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
        $scope.view_query = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/bdverificationview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbemail_type == undefined) || ($scope.rdbemail_type == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    saemail_address: $scope.txtemail_address,
                    samail_type: $scope.rdbemail_type,
                    saprimary_status: $scope.rdbprimaryemail_address,
                    sacontactinstitution_gid: $scope.sacontactinstitution_gid
                }
                var url = 'api/MstSAOnboardingInstitution/PostEmailAddressInEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
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
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbemail_type = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }
        $scope.delete_emailaddress = function (sainstitution2email_gid) {
            var params =
                {
                    sainstitution2email_gid: sainstitution2email_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteEmailAddress';
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
                emailaddress_list();
            });
        }
        function emailaddress_list() {
            var params =
            {
                sacontactinstitution_gid: $scope.sacontactinstitution_gid
            }
            var url = 'api/MstSAOnboardingInstitution/GetEmailAddressTempList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiemailaddress_list = resp.data.saOnboardInstiemailaddress_list;
            });
        }

        $scope.CICDocumentUpload = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly"); 
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingInstitution/SaInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lsfilename = resp.data.filename;
                        $scope.lsfilepath = resp.data.filepath;
                        $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.uploaddocumentcancel = function (institutionsabureaudocumentupload_gid) {
            lockUI();
            var params = {
                institutionsabureaudocumentupload_gid: institutionsabureaudocumentupload_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteBureauDocuments';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                var param = {
                    sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                };
                var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.recproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.final_approval = function () {
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';

            if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                lsassessmentagency_gid = $scope.cbosaassessmentagency
                lsassessmentagency_name = $('#assessmentagency_name :selected').text()
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating;
                lsassessmentagencyrating_name = $('#assessmentagencyrating_name :selected').text()
            }

            var satypename = $('#satype_name :selected').text();
            var saentitytypename = $('#saentitytype_name :selected').text();
            var designationtype = $('#Designation :selected').text();

         //if ($scope.txtagreementexpiry_date < $scope.txtagreementexecution_date) {
         //       Notify.alert("Samfin agreement expiry date should be later than Agreement execution date..!", 'warning');
         //   }
         //else if ($scope.txtagroagreementexpiry_date < $scope.txtagroagreementexecution_date) {
         //    Notify.alert("Samagro agreement expiry date should be later than Agreement execution date..!", 'warning');
         //}

       /*  else {*/

             var params = {
                 satype_gid: $scope.cbosatype,
                 satype_name: satypename,
                 sa_reportingmanager: $scope.txtRM,
                 saentitytype_gid: $scope.cbosaentitytype,
                 saentitytype_name: saentitytypename,
                 designation_gid: $scope.cboDesignation,
                 designation_type: designationtype,
                 sa_associatename: $scope.txtsamunnati_associate_name,
                 sa_contactfirstname: $scope.txtsacontact_person_first_name,
                 sa_contactmiddlename: $scope.txtsacontact_person_middle_name,
                 sa_contactlastname: $scope.txtsacontact_person_last_name,
                 sa_dateofincorporation: $scope.txtdateofincorporation_date,
                 sa_annualturnover: $scope.txtannual_turnover,
                 sa_companypan: $scope.txtsa_pannumber,
                 sa_companystdate: $scope.txtcompanystart_date,
                 sa_yearsinbusiness: $scope.txtyearin_business,
                 sa_monthsinbusiness: $scope.txtmonthsin_business,
                 // pan_number: $scope.txtpan_number,
                 // sa_startdate: $scope.txtstart_date,
                 // sa_enddate: $scope.txtend_date,
                 saifsc_code: $scope.txtifsc_code,
                 saaccount_number: $scope.txtbankaccount_number,
                 saaccountholder_name: $scope.txtaccountholder_name,
                 confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                 sacanccheque_number: $scope.txtcancelledcheque_number,
                 sabank_name: $scope.txtbank_name,
                 sabranch_name: $scope.txtbranch_name,
                 city: $scope.txtcity,
                 district: $scope.txtdistrict,
                 state: $scope.txtstate,
                 micr: $scope.txtmicr,
                 branch_address: $scope.txtbranch_address,

                 interviewevalution: $scope.rbointerview_evalution_form,
                 applicationform: $scope.rboapplication_form,
                 kycdocuments: $scope.rbokycdocuments,
                 vettingstatus: $scope.rbovettingstatus,
                 addressproof: $scope.rboaddressproof,
                 photographs: $scope.rbophotographs,
                 cancelledcheckleaf: $scope.rbocancelledcheckleaf,
                 agreementexecutiondate: $scope.txtagreementexecution_date,
                 agreementexpirydate: $scope.txtagreementexpiry_date,
                 agreementstatus: $scope.cboagreement_status,
                 agroagreementexecutiondate: $scope.txtagroagreementexecution_date,
                 agroagreementexpirydate: $scope.txtagroagreementexpiry_date,
                 agroagreementstatus: $scope.cboagroagreement_status,
                 sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                 verificationremarks: $scope.txtremarks,
                 sa_reportingmanager: $scope.lblsareportingmanager,
                 assessmentagency_gid: lsassessmentagency_gid,
                 assessmentagency_name: lsassessmentagency_name,
                 assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                 assessmentagencyrating_name: lsassessmentagencyrating_name,
                 ratingas_date: $scope.txtrasteason_date,
                 sa_apputr: $scope.txtutr_number,
                 sa_appcrediteddate: $scope.txtcredited_date,
                 sa_appcreditedamount: $scope.txtcredited_amount,
                 gender: $scope.cbogender,


             }
             var url = 'api/MstSAOnboardingInstitution/CheckerApprovalEdit';
             SocketService.post(url, params).then(function (resp) {
                 if (resp.data.status == true) {
                     Notify.alert(resp.data.message, {
                         status: 'success',
                         pos: 'top-center',
                         timeout: 3000
                     });
                     $state.go("app.MstSAOnboardingVerificationSummary");
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


        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtannual_turnover = "";
            }
            else {
                $scope.txtannual_turnover = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }
        $scope.credited_amountChange = function () {
            var input = document.getElementById('credited_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditedamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcredited_amount = "";
            }
            else {
                $scope.txtcredited_amount = output;
                document.getElementById('words_creditedamount').innerHTML = lswords_creditedamount;
            }
        }
        $scope.creditscoreChange = function () {
            var input = document.getElementById('bureau_score').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtbureau_score = "";
            }
            else {
                $scope.txtbureau_score = output;
            }
        }
        $scope.uploadattachment = function (val, val1, name) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {
                    frm.append(fi.files[i].name, fi.files[i])
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                }
                frm.append('project_flag', "documentformatonly"); 
                var url = 'api/OsdTrnCustomerQueryMgmt/MailAttachment';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                        var url = 'api/OsdTrnCustomerQueryMgmt/GetMailAttachment';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploaddocument = resp.data.MdlDocDetails;
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')
                    }
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }
        $scope.UploadDocCancel = function (id) {
            var params = {
                mailattachment_gid: id
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/DeleteAttachment';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document deleted Successfully..!!', 'success')
                    var url = 'api/OsdTrnCustomerQueryMgmt/GetMailAttachment';
                    SocketService.get(url).then(function (resp) {
                        $scope.uploaddocument = resp.data.MdlDocDetails;
                    });
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred')
                }
            });
        }
        $scope.DocumentUpload = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {

                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);

            frm.append('document_name', $scope.txtdocument_name);
            frm.append('project_flag', "documentformatonly"); 
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.documentupload_list = resp.data.application_list;
                    unlockUI();

                    $("#document").val('');
                    $scope.txtdocument_name = "";
                    $scope.uploadfrm = undefined;

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
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }
        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        //Bureau
        $scope.addbureau_institution = function () {
            if (($scope.cboBureauName == undefined || $scope.cboBureauName == null || $scope.cboBureauName == '') || ($scope.txtbureauscore_date == null || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
                ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
                ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtobservations == '') || ($scope.txtbureau_response == '' || $scope.txtbureau_response == undefined || $scope.txtbureau_response == null)) {

                Notify.alert('Enter All Mandatory Values', 'warning');
            }
            else {

                var lsassessmentagency_gid = '';
                var lsassessmentagency_name = '';
                var lsassessmentagencyrating_gid = '';
                var lsassessmentagencyrating_name = '';

                if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                    lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
                    lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
                }
                if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
                }


                var params = {
                    assessmentagency_gid: lsassessmentagency_gid,
                    assessmentagency_name: lsassessmentagency_name,
                    assessmentagencyrating_name: lsassessmentagencyrating_name,
                    assessmentagencyrating_gid: lsassessmentagencyrating_gid,

                    sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,
                    ratingas_date: $scope.txtrasteason_date,
                }
                var url = 'api/MstSAOnboardingInstitution/PostSABureauInstitution';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.cboBureauName = '';
                        $scope.txtbureauscore_date = '';
                        $scope.txtobservations = '';
                        $scope.txtbureau_response = '';
                        $scope.txtbureau_score = '';
                        $scope.cicuploaddoc_list = '';
                        $scope.txtrasteason_date = '';
                        $scope.cbosaassessmentagency = '';
                        $scope.cboassessmentagencyrating = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                    var params = {
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                    }
                    var url = 'api/MstSAOnboardingInstitution/GetSABureauInstitutionList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.contactbureau_list = resp.data.sainstitutebureau_list;

                    });



                });
            }
        }
        $scope.view_checkerquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaigncheckerqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.view_makerquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/makerDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.mycampaignquery_close = function (approverinstitutionraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        approverinstitutionraisequery_gid: approverinstitutionraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        sacontactinstitution_gid: sacontactinstitution_gid
                    }
                    var url = 'api/MstSAOnboardingInstitution/PostApproverInstitutionresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, queryresponse_by) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = queryresponse_by;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //$scope.bureau_view = function (sainstitution2bureau_gid) {
        //    $location.url('app/MstSAonboardingBureauView?lssainstitution2bureau_gid=' + sainstitution2bureau_gid + '&lssacontactinstitution_gid=' + $scope.sacontactinstitution_gid);
        //}


        $scope.bureau_view = function (sainstitution2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/bureau_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var param = {
                    sainstitution2bureau_gid: sainstitution2bureau_gid
                }

                var url = 'api/MstSAOnboardingInstitution/SABureauView';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;

                    $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                    $scope.txtassessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                    $scope.ratingas_date = resp.data.ratingas_date;

                    unlockUI();
                });

                var url = 'api/MstSAOnboardingInstitution/SAUploadIndDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.downloadallbureau = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }

                }
                $scope.documentbureauviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);

                }
                //$scope.downloads = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("StoryboardAPI");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}

                $scope.recproof_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }
        }


        $scope.bureau_delete = function (sainstitution2bureau_gid) {
            var params = {
                sainstitution2bureau_gid: sainstitution2bureau_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteContactBureau';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }

            });
        }

        // Mail approval doc upload
        $scope.maildocumentupload = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly"); 
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingInstitution/SaMailDocument';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/SAMailDocumentTempList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.mailuploaddoc_list = resp.data.sauploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.maildocumentcancel = function (sainstitutionmaildocument_gid) {
            lockUI();
            var params = {
                sainstitutionmaildocument_gid: sainstitutionmaildocument_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteSAMailDocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                var param = {
                    sacontactinstitution_gid: $scope.sacontactinstitution_gid
                };
                var url = 'api/MstSAOnboardingInstitution/SAMailDocumentTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mailuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        //Verification document
        $scope.verifycompanydocument = function (val, val1, name) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#companyfile").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                //frm.append('document_title', $scope.cbocompanydocumentname.sadocumentlist_name);
                //frm.append('sadocumentlist_gid', $scope.cbocompanydocumentname.sadocumentlist_gid);
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "documentformatonly"); 
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstSAOnboardingInstitution/SaVerifyDocument';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        //$scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                        unlockUI();
                        $("#companyfile").val('');
                        //$scope.cbocompanydocumentname = "";
                        $scope.txtdocument_title = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var param = {
                                sacontactinstitution_gid: $scope.sacontactinstitution_gid
                            };
                            var url = 'api/MstSAOnboardingInstitution/SaVerifyDocumentTempList';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.verifydelete = function (sainstitutionverifydocument_gid) {
            lockUI();
            var params = {
                sainstitutionverifydocument_gid: sainstitutionverifydocument_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteSaVerifyDocument';
            SocketService.getparams(url, params).then(function (resp) {
                //$scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var param = {
                    sacontactinstitution_gid: $scope.sacontactinstitution_gid
                };
                var url = 'api/MstSAOnboardingInstitution/SaVerifyDocumentTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.instverificationdocument_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
    }
})();
