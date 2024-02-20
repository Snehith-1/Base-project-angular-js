(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCreditCrimeCheckRecordAPIController', MstCADCreditCrimeCheckRecordAPIController);

    MstCADCreditCrimeCheckRecordAPIController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function MstCADCreditCrimeCheckRecordAPIController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCreditCrimeCheckRecordAPIController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {

            var params = {
                contact_gid: $scope.contact_gid
            }

            var url = 'api/CADCrimeCheckAPI/GetCrimeRecordIndividualDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblindividual_name = resp.data.individual_name;
                $scope.lblindividual_dob = resp.data.individual_dob;
                $scope.lblfather_name = resp.data.individual_fathername;
                $scope.individualaddress_list = resp.data.individualaddress_list;
            });

            var url = 'api/CADCrimeCheckAPI/GetTaggedCaseIndividualSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tagcaseindividual_list = resp.data.tagcaseindividual_list;

            });


            $scope.rdbSearchmode = "Default";
        }

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.individual_addguarantee = function () {
            $location.url('app/MstCADIndividualGuaranteeDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/MstCADIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/MstCADIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/MstCADCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/MstCADCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/MstCADCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/MstCADCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_repayment = function () {
            $location.url('app/MstCADCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/MstCADCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.creditbankacctdtl_edit = function (creditbankdtl_gid) {
            $location.url('app/MstCADCreditIndividualBankAcctEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&creditbankdtl_gid=' + creditbankdtl_gid + '&lspage=' + lspage);
        }
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCADCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCADCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/MstCADCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }

        $scope.CrimeCheck_View = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/CrimeCheckView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lblcinNumber = data.cinNumber;
                $scope.lblcaseType = data.caseType;
                $scope.lblcaseStatus = data.caseStatus;
                $scope.lblpetitioner = data.petitioner;
                $scope.lblpetitionerAddress = data.petitionerAddress;
                $scope.lblrespondent = data.respondent;
                $scope.lblrespondentAddress = data.respondentAddress;
                $scope.lblcaseTypeName = data.caseTypeName;
                $scope.lblcaseName = data.caseName
                $scope.lblcourtType = data.courtType;
                $scope.lbldistrict = data.district;
                $scope.lblstate = data.state;
                $scope.lblyear = data.year;
                $scope.lblgfc_updated_at = data.gfc_updated_at;
                $scope.lblgfc_uniqueid = data.gfc_uniqueid;




                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.CrimeCheck_Tag = function (data) {
            
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to tag this Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Tag it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var params = {
                        contact_gid: $scope.contact_gid,
                        cin_number: data.cinNumber,
                        case_type: data.caseType,
                        case_status: data.caseStatus,
                        petitioner: data.petitioner,
                        petitioner_address: data.petitionerAddress,
                        respondent: data.respondent,
                        respondent_address: data.respondentAddress,
                        casetype_name: data.caseTypeName,
                        case_name: data.caseName,
                        court_type: data.courtType,
                        district: data.district,
                        state: data.state,
                        year: data.year,
                        gfc_updated_at: data.gfc_updated_at,
                        gfc_uniqueid: data.gfc_uniqueid,
                        casedetails_link: data.caseDetailsLink
                    }
                    lockUI();
                    var url = 'api/CADCrimeCheckAPI/TagCaseIndividual';
                    SocketService.post(url, params).then(function (resp) {
                       unlockUI();
                       if (resp.data.status == true) {
                           SweetAlert.swal('Case Tagged Successfully!');     
                           taggedcaseindividual_list();        
                           $scope.tagged_cases = true;
                           $scope.search_results = false;           
                       }
                       else {
                           alert(resp.data.message, {
                               status: 'warning',
                               pos: 'top-center',
                               timeout: 3000
                           });   
                       }
                    });
                }
                $scope.search_results = true;
            });
        }

        function taggedcaseindividual_list() {
            var params = {
                contact_gid: $scope.contact_gid
            }
            var url = 'api/CADCrimeCheckAPI/GetTaggedCaseIndividualSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tagcaseindividual_list = resp.data.tagcaseindividual_list;
            });
        }

        $scope.TaggedCase_View = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/TaggedCaseView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lblcinNumber = data.cin_number;
                $scope.lblcaseType = data.case_type;
                $scope.lblcaseStatus = data.case_status;
                $scope.lblpetitioner = data.petitioner;
                $scope.lblpetitionerAddress = data.petitioner_address;
                $scope.lblrespondent = data.respondent;
                $scope.lblrespondentAddress = data.respondent_address;
                $scope.lblcaseTypeName = data.casetype_name;
                $scope.lblcaseName = data.case_name
                $scope.lblcourtType = data.court_type;
                $scope.lbldistrict = data.district;
                $scope.lblstate = data.state;
                $scope.lblyear = data.year;
                $scope.lblgfc_updated_at = data.gfc_updated_at;
                $scope.lblgfc_uniqueid = data.gfc_uniqueid;
                $scope.lblcasedetails_link = data.casedetails_link;
                




                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.TaggedCase_Delete = function (crimecasetaggedcontact_gid) {
            var params =
                {
                    crimecasetaggedcontact_gid: crimecasetaggedcontact_gid
                }
            var url = 'api/CADCrimeCheckAPI/DeleteTaggedCaseIndividual';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    taggedcaseindividual_list();
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

        $scope.reset_crimecheck = function () {
            $scope.cboaddress = '';
            $scope.rdbSearchmode = '';
            $scope.searchin_petitioner = '';
            $scope.searchin_respondent = '';
        }

        $scope.SearchResults = function () {
            $scope.search_results = true;
            $scope.tagged_cases = false;
        }

        $scope.TaggedCases = function () {
            $scope.tagged_cases = true;
            $scope.search_results = false;
        }
     

        $scope.search_crimecheck = function () {

            var searchIn = '';
            if ($scope.searchin_petitioner == true && $scope.searchin_respondent == true) {
                searchIn = 'petitioner,respondent';
            } else if ($scope.searchin_petitioner == true && ($scope.searchin_respondent == undefined || $scope.searchin_respondent == false)) {
                searchIn = 'petitioner';
            } else if ($scope.searchin_respondent == true && ($scope.searchin_petitioner == undefined || $scope.searchin_petitioner == false)) {
                searchIn = 'respondent';
            } else {

            }

            if ($scope.cboaddress == undefined || $scope.rdbSearchmode == undefined || searchIn == '') {
                Notify.alert('Kindly enter all the mandatory fields..!', 'warning');
            }
            else {
                var params = {
                    individual_name: $scope.lblindividual_name,
                    father_name: $scope.lblfather_name,
                    application_gid: $scope.application_gid,
                    contact_gid: $scope.contact_gid,
                    individual_dob: $scope.lblindividual_dob,
                    individual_address: $scope.cboaddress.address,
                    search_mode: $scope.rdbSearchmode,
                    search_in: searchIn
                }
                var url = 'api/CADCrimeCheckAPI/GetCrimeRecordsIndividual';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    setTimeout(function () {
                        if (resp.data.requestStatus == true) {
                            $scope.case_list = '';
                            $scope.case_list = resp.data.details;

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.search_results = true;
                            $scope.tagged_cases = false;
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    }, 1000);


                });
                
            }

        }

        $scope.highlight = function(data, searchTerm) {
            var text = '';
            var text = data.petitioner + ' Vs ' + data.respondent + ' <br /> #' + data.cinNumber + '@' + data.courtType + ',' + data.district;
            searchTerm = searchTerm.trim();
            if (!searchTerm) {
                return $sce.trustAsHtml(text);
            } 
            else {
                 var searchTermArray = searchTerm.split(' ');
                 for (var i = 0; i < searchTermArray.length; i++) { 
                    text = text.replace(new RegExp(searchTermArray[i], 'gi'), '<span class="highlightedText">$&</span>');
                    }
                return $sce.trustAsHtml(text);
            }

            
        };

    }
})();
