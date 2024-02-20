(function () {
    'use strict';

    angular
        .module('angle')
        .controller('ComprehensiveDetailsViewController', ComprehensiveDetailsViewController);

    ComprehensiveDetailsViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function ComprehensiveDetailsViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ComprehensiveDetailsViewController';        
        var institutionprobedetails_gid = $location.search().institutionprobedetails_gid;

        activate();

        function activate() {
            $scope.lsdetail_name = $location.search().lsdetail_name;
            var params = {
                institutionprobedetails_gid: institutionprobedetails_gid,
            }

            var url = 'api/ProbeAPI/ComprehensiveDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();


                $scope.cin = resp.data.data.company.cin;
                $scope.legal_name = resp.data.data.company.legal_name;
                $scope.efiling_status = resp.data.data.company.efiling_status;
                $scope.incorporation_date = resp.data.data.company.incorporation_date;
                $scope.paid_up_capital = resp.data.data.company.paid_up_capital;
                $scope.sum_of_charges = resp.data.data.company.sum_of_charges;
                $scope.authorized_capital = resp.data.data.company.authorized_capital;
                $scope.active_compliance = resp.data.data.company.active_compliance;
                $scope.cirp_status = resp.data.data.company.cirp_status;
                $scope.registered_address = resp.data.data.company.registered_address.address_line1 + ',' + resp.data.data.company.registered_address.address_line2 + ',' + resp.data.data.company.registered_address.city + ',' + resp.data.data.company.registered_address.pincode + ',' + resp.data.data.company.registered_address.state;
                $scope.business_address = resp.data.data.company.business_address.address_line1 + ',' + resp.data.data.company.business_address.address_line2 + ',' + resp.data.data.company.business_address.city + ',' + resp.data.data.company.business_address.pincode + ',' + resp.data.data.company.business_address.state;
                $scope.classification = resp.data.data.company.classification;
                $scope.status = resp.data.data.company.status;
                $scope.next_cin = resp.data.data.company.next_cin;
                $scope.last_agm_date = resp.data.data.company.last_agm_date;
                $scope.last_filing_date = resp.data.data.company.last_filing_date;
                $scope.email = resp.data.data.company.email;
                $scope.desc_thousand_char = resp.data.data.description.desc_thousand_char;

                $scope.namehistory_list = resp.data.data.name_history;


                $scope.authorizedsignatories_list = resp.data.data.authorized_signatories;

                $scope.directornetwork_list = resp.data.data.director_network;
                
                $scope.contactdetailsemail_list = resp.data.data.contact_details.email;
                $scope.contactdetailsphone_list = resp.data.data.contact_details.phone;

                $scope.opencharges_list = resp.data.data.open_charges;
                $scope.openchargeslatestevent_list = resp.data.data.open_charges_latest_event;
                $scope.chargesequence_list = resp.data.data.charge_sequence;

                $scope.financials_list = resp.data.data.financials;
                $scope.nbfcfinancials_list = resp.data.data.nbfc_financials;

                $scope.financialparameters_list = resp.data.data.financial_parameters;

                $scope.industrysegments_list = resp.data.data.industry_segments;

                $scope.principalbusinessactivities_list = resp.data.data.principal_business_activities;

                $scope.shareholdings_list = resp.data.data.shareholdings;
                $scope.shareholdingssummary_list = resp.data.data.shareholdings_summary;
                $scope.directorshareholdings_list = resp.data.data.director_shareholdings;

                $scope.bifrhistory_list = resp.data.data.bifr_history;
                $scope.cdrhistory_list = resp.data.data.cdr_history;
                $scope.defaulter_list = resp.data.data.defaulter_list;

                $scope.legalhistory_list = resp.data.data.legal_history;

                $scope.creditratings_list = resp.data.data.credit_ratings;

                $scope.holdingentitycompany_list = resp.data.data.holding_entities.company;
                $scope.holdingentityllp_list = resp.data.data.holding_entities.llp;
                $scope.holdingentityothers_list = resp.data.data.holding_entities.others;

                $scope.subsidiaryentitycompany_list = resp.data.data.subsidiary_entities.company;
                $scope.subsidiaryentityllp_list = resp.data.data.subsidiary_entities.llp;
                $scope.subsidiaryentityothers_list = resp.data.data.subsidiary_entities.others;

                $scope.associateentitycompany_list = resp.data.data.associate_entities.company;
                $scope.associateentityllp_list = resp.data.data.associate_entities.llp;
                $scope.associateentityothers_list = resp.data.data.associate_entities.others;

                $scope.jointventurecompany_list = resp.data.data.joint_ventures.company;
                $scope.jointventurellp_list = resp.data.data.joint_ventures.llp;
                $scope.jointventureothers_list = resp.data.data.joint_ventures.others;

                $scope.securitiesallotment_list = resp.data.data.securities_allotment;

                $scope.peercomparison_list = resp.data.data.peer_comparison;
                $scope.gstdetails_list = resp.data.data.gst_details;
                $scope.struckoff248_details = resp.data.data.struckoff248_details;

            });

        }

        $scope.basic_detail = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=BASICDETAIL');
            activate(); window.scroll(0, 0)
        }

        $scope.authorized_signatories = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=AUTHORIZEDSIGNATORIES');
            activate(); window.scroll(0, 0)
        }

        $scope.director_network = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=DIRECTORNETWORK');
            activate(); window.scroll(0, 0)
        }

        $scope.contact_details = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=CONTACTDETAILS');
            activate(); window.scroll(0, 0)
        }

        $scope.open_charges = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=OPENCHARGES');
            activate(); window.scroll(0, 0)
        }

        $scope.charge_sequence = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=CHARGESEQUENCE');
            activate(); window.scroll(0, 0)
        }

        $scope.financials = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=FINANCIALS');
            activate(); window.scroll(0, 0)
        }

        $scope.nbfc_financials = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=NBFCFINANCIALS');
            activate(); window.scroll(0, 0)
        }

        $scope.financial_parameters = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=FINANCIALPARAMETERS');
            activate(); window.scroll(0, 0)
        }

        $scope.industry_segments = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=INDUSTRYSEGMENTS');
            activate(); window.scroll(0, 0)
        }

        $scope.principal_businessactivities = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=PRINCIPALBUSINESSACTIVITIES');
            activate(); window.scroll(0, 0)
        }
        $scope.shareholdings = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=SHAREHOLDINGS');
            activate(); window.scroll(0, 0)
        }

        $scope.shareholdings_summary = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=SHAREHOLDINGSSUMMARY');
            activate(); window.scroll(0, 0)
        }

        $scope.director_shareholdings = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=DIRECTORSHAREHOLDINGS');
            activate(); window.scroll(0, 0)
        }

        $scope.bifr_history = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=BIFRHISTORY');
            activate(); window.scroll(0, 0)
        }

        $scope.cdr_history = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=CDRHISTORY');
            activate(); window.scroll(0, 0)
        }

        $scope.defaulters_list = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=DEFAULTERLIST');
            activate(); window.scroll(0, 0)
        }

        $scope.legal_history = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=LEGALHISTORY');
            activate(); window.scroll(0, 0)
        }

        $scope.credit_ratings = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=CREDITRATINGS');
            activate(); window.scroll(0, 0)
        }

        $scope.holding_entities = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=HOLDINGENTITIES');
            activate(); window.scroll(0, 0)
        }

        $scope.subsidiary_entities = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=SUBSIDIARYENTITIES');
            activate(); window.scroll(0, 0)
        }

        $scope.associate_entities = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=ASSOCIATEENTITIES');
            activate(); window.scroll(0, 0)
        }

        $scope.joint_ventures = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=JOINTVENTURES');
            activate(); window.scroll(0, 0)
        }

        $scope.securities_allotment = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=SECURITIESALLOTMENT');
            activate(); window.scroll(0, 0)
        }

        $scope.peer_comparison = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=PEERCOMPARISON');
            activate(); window.scroll(0, 0)
        }

        $scope.gst_details = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=GSTDETAILS');
            activate(); window.scroll(0, 0)
        }

        $scope.struckoff248_details = function () {
            $location.url('app/ComprehensiveDetailsView?lsdetail_name=STRUCKOFF248DETAILS');
            activate(); window.scroll(0, 0)
        }

        

        $scope.close = function () {
            window.close();
        }
    }
})();
