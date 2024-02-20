
/* ---------------Start - Task Supplier Cheque Management 2022-07-11 12:25 --------------- */

create table  agr_mst_tsuprudcmanagement2cheque as select * from agr_mst_tudcmanagement2cheque;

create table agr_mst_tsuprcheque2document as select * from agr_mst_tcheque2document;

create table agr_mst_tsuprudcmanagement as select * from agr_mst_tudcmanagement;

create table agr_mst_tsuprudcmanagement2chequeupdatelog as select * from agr_mst_tudcmanagement2chequeupdatelog;

create table agr_trn_tsuprapplication2chequelist as select * from agr_trn_tapplication2chequelist;

/* ---------------End - Task Supplier Cheque Management 2022-07-11 12:25  --------------- */ 
 
/* ---------------Start - Task creditor sequence increment flage 2022-07-11 16:48 --------------- */

UPDATE `adm_mst_tsequence` SET `sequence_flag` = 'Y' WHERE (`sequence_gid` = 'SSQM2022070200025');

/* ---------------End - Task creditor sequence increment flag 2022-07-11 16:48 --------------- */

 
/* ---------------Start - Task Master change reflect to Transaction Pending Business Approval application 2022-07-11 20:25 --------------- */

CREATE TABLE `agr_mst_tapplicationlog` (
   `applicationlog_gid` int(11) NOT NULL AUTO_INCREMENT,
   `application_gid` varchar(45) DEFAULT NULL,
   `application_no` varchar(64) DEFAULT NULL,
   `customer_urn` varchar(30) DEFAULT NULL,
   `customer_name` longtext,
   `vertical_gid` varchar(45) DEFAULT NULL,
   `vertical_name` varchar(120) DEFAULT NULL,
   `baselocation_gid` varchar(45) DEFAULT NULL,
   `baselocation_name` varchar(256) DEFAULT NULL,
   `cluster_gid` varchar(45) DEFAULT NULL,
   `cluster_name` varchar(256) DEFAULT NULL,
   `region_gid` varchar(45) DEFAULT NULL,
   `region_name` varchar(256) DEFAULT NULL,
   `zone_gid` varchar(45) DEFAULT NULL,
   `zone_name` varchar(256) DEFAULT NULL,
   `relationshipmanager_gid` varchar(45) DEFAULT NULL,
   `relationshipmanager_name` varchar(120) DEFAULT NULL,
   `drm_gid` varchar(45) DEFAULT NULL,
   `drm_name` varchar(120) DEFAULT NULL,
   `clustermanager_gid` varchar(45) DEFAULT NULL,
   `clustermanager_name` varchar(120) DEFAULT NULL,
   `regionalhead_gid` varchar(45) DEFAULT NULL,
   `regionalhead_name` varchar(120) DEFAULT NULL,
   `zonalhead_gid` varchar(45) DEFAULT NULL,
   `zonalhead_name` varchar(120) DEFAULT NULL,
   `businesshead_gid` varchar(45) DEFAULT NULL,
   `businesshead_name` varchar(120) DEFAULT NULL,
   `head_change` varchar(45) DEFAULT NULL,
   `created_by` varchar(45) DEFAULT NULL,
   `created_date` datetime DEFAULT NULL,
   `program_gid` varchar(45) DEFAULT NULL,
   `program_name` varchar(256) DEFAULT NULL,
   PRIMARY KEY (`applicationlog_gid`)
 ) ;


 CREATE TABLE `agr_mst_tsuprapplicationlog` (
   `applicationlog_gid` int(11) NOT NULL AUTO_INCREMENT,
   `application_gid` varchar(45) DEFAULT NULL,
   `application_no` varchar(64) DEFAULT NULL,
   `customer_urn` varchar(30) DEFAULT NULL,
   `customer_name` longtext,
   `vertical_gid` varchar(45) DEFAULT NULL,
   `vertical_name` varchar(120) DEFAULT NULL,
   `baselocation_gid` varchar(45) DEFAULT NULL,
   `baselocation_name` varchar(256) DEFAULT NULL,
   `cluster_gid` varchar(45) DEFAULT NULL,
   `cluster_name` varchar(256) DEFAULT NULL,
   `region_gid` varchar(45) DEFAULT NULL,
   `region_name` varchar(256) DEFAULT NULL,
   `zone_gid` varchar(45) DEFAULT NULL,
   `zone_name` varchar(256) DEFAULT NULL,
   `relationshipmanager_gid` varchar(45) DEFAULT NULL,
   `relationshipmanager_name` varchar(120) DEFAULT NULL,
   `drm_gid` varchar(45) DEFAULT NULL,
   `drm_name` varchar(120) DEFAULT NULL,
   `clustermanager_gid` varchar(45) DEFAULT NULL,
   `clustermanager_name` varchar(120) DEFAULT NULL,
   `regionalhead_gid` varchar(45) DEFAULT NULL,
   `regionalhead_name` varchar(120) DEFAULT NULL,
   `zonalhead_gid` varchar(45) DEFAULT NULL,
   `zonalhead_name` varchar(120) DEFAULT NULL,
   `businesshead_gid` varchar(45) DEFAULT NULL,
   `businesshead_name` varchar(120) DEFAULT NULL,
   `head_change` varchar(45) DEFAULT NULL,
   `created_by` varchar(45) DEFAULT NULL,
   `created_date` datetime DEFAULT NULL,
   `program_gid` varchar(45) DEFAULT NULL,
   `program_name` varchar(256) DEFAULT NULL,
   PRIMARY KEY (`applicationlog_gid`)
 ) ;


/* ---------------End - TaskMaster change reflect to Transaction Pending Business Approval application 2022-07-11 20:25 --------------- */

/* ---------------Start - Task Product against Service charge & Trade - Delete validation 2022-07-19 11:04 --------------- */
alter table agr_mst_tapplicationservicecharge
 add column application2loan_gid varchar(45) default null after application_gid;
 
  alter table agr_mst_tsuprapplicationservicecharge
 add column application2loan_gid varchar(45) default null after application_gid;
 
/* ---------------End -Task Product against Service charge & Trade - Delete validation 2022-07-19 11:04 --------------- */

/* ---------------Start - Task - Add column in other members log table 2022-07-21 17:40 --------------- */

alter table agr_mst_tccmeeting2othermemberslog add column ccmeeting2othermembers_gid varchar(45) DEFAULT NULL after ccmeeting2othermemberslog_gid;
alter table agr_mst_tccmeeting2othermemberslog drop column attenance_status;
/* ---------------End - Task - Add column in other members log table 2022-07-21 17:40 --------------- */

/* ---------------Start - Task - Aadhaar No Insert into creditor master  2022-07-22 17:05 --------------- */
Alter table agr_mst_tcreditor add column aadhar_no varchar(15) Default Null;
 alter table agr_mst_tcreditorupdatelog add column aadhar_no varchar(15) Default Null;
/* ---------------End - Task - Aadhaar No Insert into creditor master  2022-07-22 17:05 --------------- */

/* ---------------Start - Task Name - warehouse type include in warehouse master  2022-07-25 15:05 --------------- */
alter table agr_mst_twarehouse add column typeofwarehouse_gid varchar(45) default null ;

alter table agr_mst_twarehouse add column typeofwarehouse_name varchar(256) default null ;

alter table agr_mst_twarehouseupdatelog add column typeofwarehouse_gid varchar(45) default null ;

alter table agr_mst_twarehouseupdatelog add column typeofwarehouse_name varchar(256) default null ;
/* ---------------End - Task Name - warehouse type include in warehouse master  2022-07-25 15:05 --------------- */

/* ---------------Start - Task Name - Commodity Master - Incremental fields  2022-07-25 15:05 --------------- */

alter table ocs_mst_tvariety
  add column typeofsupplynature_gid varchar(45) default null,
  add column typeofsupplynature_name varchar(256) default null,
  add column sectorclassification_gid varchar(45) default null,
  add column sectorclassification_name varchar(256) default null;
  
  insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207250042', 'CGTS', 'commoditygststatus_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tcommoditygststatus(
  commoditygststatus_gid varchar(45) primary key,
  IGST_percent varchar(12) default null,
  SGST_percent varchar(12) default null,
  CGST_percent varchar(12) default null,
  CESS_percent varchar(12) default null,
  wef_date date default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207250043', 'CTPG', 'commoditytradeproductdtl_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tcommoditytradeproductdtl(
  commoditytradeproductdtl_gid varchar(45) primary key,
  product_gid varchar(45) default null,
  product_name varchar(246) default null,
  subproduct_gid varchar(45) default null,
  subproduct_name varchar(246) default null,
  insurancecompany_gid varchar(45) default null,
  insurancecompany_name varchar(246) default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
  insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207250044', 'CDOG', 'commoditydocument_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tcommoditydocument(
  commoditydocument_gid varchar(45) primary key,
  ason_date date default null,
  commodityreport_filename varchar(246) default null,
  commodityreport_filepath varchar(246) default null,
  riskanalysisreport_filename varchar(246) default null,
  riskanalysisreport_filepath varchar(246) default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  

   alter table agr_mst_tcommoditygststatus
  add column product_gid varchar(45) default null after commoditygststatus_gid,
  add column variety_gid varchar(45) default null after product_gid;

   alter table agr_mst_tcommoditytradeproductdtl
  add column mstproduct_gid varchar(45) default null after commoditytradeproductdtl_gid,
  add column variety_gid varchar(45) default null after mstproduct_gid;

   alter table agr_mst_tcommoditydocument
  add column product_gid varchar(45) default null after commoditydocument_gid,
  add column variety_gid varchar(45) default null after product_gid;
  
alter table agr_mst_tcommoditytradeproductdtl
  add column insurancepolicy_gid varchar(45) default null,
  add column insurancepolicy_name varchar(500) default null;
  /* ---------------End - Task Name - Commodity Master - Incremental fields  2022-07-25 15:05 --------------- */


  /* ---------------Start - Task Name - New Field Applicant Name Inserting into Warehouse  2022-07-26 15:05--------------- */



alter table agr_mst_twarehouse  add column Applicant_name varchar(64)  Default Null;
alter table agr_mst_twarehouse  add column creditor_gid varchar(45)  Default Null;
alter table agr_mst_twarehouseupdatelog add column Applicant_name varchar(64)  Default Null;
alter table agr_mst_twarehouseupdatelog add column creditor_gid varchar(45)  Default Null;


/* ---------------End - Task Name - New Field Applicant Name Inserting into Warehouse  2022-07-26 15:05--------------- */

  /* ---------------Start - Task Name - Financial details and overall limit date fields  2022-07-29 11:48--------------- */
alter table agr_mst_tapplication add column validityto_date datetime default null after validityoveralllimit_days ;

alter table agr_mst_tapplication add column validityfrom_date datetime default null after validityoveralllimit_days ;

alter table agr_mst_tapplication add column incometax_returnsstatus varchar(45) default null;

alter table agr_mst_tapplication add column revenue double (20,2) default null;

alter table agr_mst_tapplication add column profit double (20,2) default null;

alter table agr_mst_tapplication add column fixed_assets double (20,2) default null;

alter table agr_mst_tapplication add column sundrydebt_adv double (20,2) default null;

#2022-07-29

alter table agr_mst_tinstitution add column incometax_returnsstatus varchar(45) default null;

alter table agr_mst_tinstitution add column revenue double (20,2) default null;

alter table agr_mst_tinstitution add column profit double (20,2) default null;

alter table agr_mst_tinstitution add column fixed_assets double (20,2) default null;

alter table agr_mst_tinstitution add column sundrydebt_adv double (20,2) default null;



alter table agr_mst_tinstitutionupdatelog add column incometax_returnsstatus varchar(45) default null;

alter table agr_mst_tinstitutionupdatelog add column revenue double (20,2) default null;

alter table agr_mst_tinstitutionupdatelog add column profit double (20,2) default null;

alter table agr_mst_tinstitutionupdatelog add column fixed_assets double (20,2) default null;

alter table agr_mst_tinstitutionupdatelog add column sundrydebt_adv double (20,2) default null;


/* ---------------End - Task Name - Financial details and overall limit date fields  2022-07-29 11:48--------------- */

 /* ---------------Start - Task Name - Buyer Incremental - Product Details & Trade Details  2022-07-29 11:48--------------- */
 insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207290056', 'PTC', 'paymenttypecustomer_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

create table agr_mst_tapploan2paymenttypecustomer
(
paymenttypecustomer_gid varchar(45) primary key,
application_gid varchar(45) default null,
application2loan_gid varchar(45) default null,
customerpaymenttype_gid  varchar(45) default null,
customerpaymenttype_name varchar(245) default null,
maximumpercent_paymentterm varchar(10) default null,
created_by  varchar(45) default null,
created_date datetime default null
);

  insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207290057', 'APGT', 'appproduct2commoditygststatus_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tappproduct2commoditygststatus(
  appproduct2commoditygststatus_gid varchar(45) primary key,
  commoditygststatus_gid varchar(45) default null,
  application2product_gid varchar(45) default null,
  product_gid varchar(45) default null,
  variety_gid varchar(45) default null,
  IGST_percent varchar(12) default null,
  SGST_percent varchar(12) default null,
  CGST_percent varchar(12) default null,
  CESS_percent varchar(12) default null,
  wef_date date default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
  insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207290058', 'ACTP', 'appproduct2commoditytradeproductdtl_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tappproduct2commoditytradedtl(
  appproduct2commoditytrade_gid varchar(45) primary key,
  commoditytradeproductdtl_gid varchar(45) default null,
  application2product_gid varchar(45) default null,
  mstproduct_gid varchar(45) default null,
  variety_gid varchar(45) default null,
  product_gid varchar(45) default null,
  product_name varchar(246) default null,
  subproduct_gid varchar(45) default null,
  subproduct_name varchar(246) default null,
  insurancecompany_gid varchar(45) default null,
  insurancecompany_name varchar(246) default null,
  insurancepolicy_gid varchar(45) default null,
  insurancepolicy_name varchar(500) default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207290059', 'APCD', 'appproductcommoditydocument_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tappproduct2commoditydocument(
  appproduct2commoditydocument_gid varchar(45) primary key,
  commoditydocument_gid varchar(45)  default null,
  application2product_gid varchar(45) default null,
  ason_date date default null,
  commodityreport_filename varchar(246) default null,
  commodityreport_filepath varchar(246) default null,
  riskanalysisreport_filename varchar(246) default null,
  riskanalysisreport_filepath varchar(246) default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
  insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207290060', 'ALSP', 'apploan2supplierpayment_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tapploan2supplierpayment(
  apploan2supplierpayment_gid varchar(45) primary key,
  application_gid varchar(45) default null,
  application2loan_gid varchar(45) default null,
  commodity_gid varchar(45)  default null,
  commodity_name varchar(246)  default null,
  supplierpayment_type varchar(45)  default null,
  supplierpayment_typegid varchar(246) default null,
  maxpercent_paymentterm varchar(10) default null, 
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202207290061', 'ALSD', 'apploan2supplierdtl_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

  create table agr_mst_tapploan2supplierdtl(
  apploan2supplierdtl_gid varchar(45) primary key,
  application_gid varchar(45) default null,
  application2loan_gid varchar(45) default null,
  supplier_gid varchar(45) default null,
  supplier_name varchar(45) default null,
  supplier_address varchar(246)  default null,
  supplier_emailid varchar(246)  default null,
  supplier_phoneno varchar(20)  default null,
  supplier_gstno varchar(100)  default null,
  supplier_pandtl varchar(100)  default null,
  milestone_applicable varchar(5) default null, 
  milestonepaymenttype_gid varchar(5) default null, 
  milestonepaymenttype_name varchar(5) default null, 
  supplier_vintage varchar(15) default null,
  created_by varchar(45) default null,
  created_date datetime default null
  );
  
alter table ocs_mst_tvariety
add column varietysector_gid varchar(45) default null,
add column varietysector_name varchar(245) default null,
add column varietycategory_gid varchar(45) default null,
add column varietycategory_name varchar(245) default null,
add column headingdesc_product text default null;

alter table agr_mst_tapplication2loan
add column programlimit_validdfrom date default null,
add column programlimit_validdto date default null,
add column programoverall_limit varchar(15) default null;

alter table agr_mst_tapplication2loan
add column trade_orginatedby varchar(45) default null,
add column SA_Brokerage varchar(15) default null; 

alter table agr_mst_tapplication2product
add column milestone_applicability char(4) default null,
add column insurance_applicability char(4) default null,
add column milestonepayment_gid varchar(45) default null,
add column milestonepayment_name varchar(256) default null,
add column sa_payout varchar(15) default null,
add column insurance_availability double(13,2) default 0,
add column insurance_percent varchar(45) default null,
add column insurance_cost double(13,2) default 0,
add column net_yield double(13,2) default 0;
 
alter table agr_mst_tapplication2loan
drop column milestone_applicability,
drop column insurance_applicability,
drop column milestonepayment_gid,
drop column milestonepayment_name,
drop column sa_payout,
drop column insurance_availability,
drop column insurance_percent,
drop column insurance_cost,
drop column net_yield;
  

  
 alter table agr_mst_tapplication2product
 add column markto_marketvalue varchar(5) default null,
 add column pricereference_source text default null,
 add column headingdesc_product  text default null,
 add column typeofsupply_naturegid  varchar(45) default null,
 add column typeofsupply_naturename  varchar(246) default null,
  add column sectorclassification_gid  varchar(45) default null,
 add column sectorclassification_name  varchar(246) default null,
  add column creditperiod_years  varchar(10) default null,
  add column creditperiod_months  varchar(10) default null,
  add column creditperiod_days  varchar(10) default null,
  add column overallcreditperiod_limit  varchar(100) default null,
  add column commodity_margin varchar(20) default null;
   

alter table agr_mst_tapplication2loan
add column holding_periods varchar(15) default null,
add column holdingmonthly_procurement  varchar(15) default null,
add column extendedholding_periods varchar(15) default null,
add column extendedmonthly_procurement varchar(15) default null,
add column charges_extendedperiod varchar(15) default null,
  add column customer_advance varchar(15) default null,
add column reimburesementof_expenses  text default null,
  add column reimburesementof_expensespenalty text default null,
  add column bankfundingdata_filename  varchar(246) default null,
  add column bankfundingdata_filepath  varchar(246) default null,
  add column needfor_stocking  text default null,
  add column product_portfolio  text default null,
  add column production_capacity  text default null,
  add column natureof_operations text default null,
  add column averagemonthly_inventoryholding text default null,
  add column financialinstitutions_relationship text default null;

  create table agr_tmp_tbankfundingdataupload
  (
  bankfundingdataupload_gid int NOT NULL AUTO_INCREMENT,
  application_gid varchar(45) default null,
  application2loan_gid varchar(45) default null,
  file_name varchar(246) default null,
  file_path varchar(246) default null,
  created_by varchar(45) default null,
  created_date datetime default null,
  PRIMARY KEY (bankfundingdataupload_gid)
  );


  create table agr_mst_tapp2suppliergstdtl(
app2suppliergstdtl_gid varchar(45) not null primary key,
institution2branch_gid  varchar(45) default null,
gst_state varchar(200) default null,
gst_no varchar(200) default null,
created_by varchar(45) default null,
created_date datetime default null
);



insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208040064', 'A2SG', 'app2suppliergstdtl_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);



alter table agr_mst_tapp2suppliergstdtl
add column apploan2supplierdtl_gid varchar(45) default null after app2suppliergstdtl_gid;

alter table agr_mst_tapploan2supplierdtl
Modify   milestonepaymenttype_gid varchar(45) default null,
Modify   milestonepaymenttype_name varchar(246) default null;

alter table agr_mst_tapplication2product
add column commoditynet_yield varchar(45) default null;

alter table agr_mst_tapplication2product
Modify markto_marketvalue varchar(15) default null;

alter table agr_mst_tappproduct2commoditydocument
add column variety_gid varchar(45) default null;

alter table agr_mst_tapplication2loan
modify programoverall_limit varchar(100) default null;

  /* ---------------End - Task Name - Buyer Incremental - Product Details & Trade Details  2022-07-29 11:48--------------- */
  
   /* ---------------Start - Task Name -  CC Report   2022-08-01 11:48--------------- */
  alter table agr_mst_tccmeeting2memberslog
add column
`approvalinitiate_by` varchar(45) DEFAULT NULL;
 /* ---------------End - Task Name -  CC Report   2022-08-01 11:48--------------- */
 
 /* ---------------Start - Task Name -   Add Tan Number field in company info page 2022-08-02 11:48--------------- */
 alter table agr_mst_tinstitution add column tan_number varchar(45) default null; 
 alter table agr_mst_tinstitutionupdatelog add column tan_number varchar(45) default null;
/* ---------------End - Task Name -  Add Tan Number field in company info page 2022-08-02 11:48--------------- */

/* ---------------Start - Task Name -   Bank details Multiple Add at Company Info page 2022-08-02 11:48--------------- */
CREATE TABLE `agr_mst_tinstitution2bankdtl` (
    `institution2bankdtl_gid` VARCHAR(45) NOT NULL,
    `institution_gid` VARCHAR(45) DEFAULT NULL,
    `application_gid` VARCHAR(45) DEFAULT NULL,
    `bankaccount_number` VARCHAR(64) DEFAULT NULL,
    `ifsc_code` VARCHAR(45) DEFAULT NULL,
    `bank_name` VARCHAR(256) DEFAULT NULL,
    `branch_name` VARCHAR(256) DEFAULT NULL,
    `micr_code` VARCHAR(45) DEFAULT NULL,
    `accountholder_name` VARCHAR(50) DEFAULT NULL,
    `accounttype_gid` VARCHAR(45) DEFAULT NULL,
    `accounttype_name` VARCHAR(45) DEFAULT NULL,
    `joint_account` CHAR(1) DEFAULT NULL,
    `jointaccountholder_name` VARCHAR(50) DEFAULT NULL,
    `chequebookfacility_available` CHAR(1) DEFAULT NULL,
    `accountopen_date` DATETIME DEFAULT NULL,
    `created_by` VARCHAR(45) DEFAULT NULL,
    `created_date` DATETIME DEFAULT NULL,
    `updated_by` VARCHAR(45) DEFAULT NULL,
    `updated_date` DATETIME DEFAULT NULL,
    `bank_address` LONGTEXT,
    `bankaccount_name` VARCHAR(128) DEFAULT NULL,
    `bankaccounttype_gid` VARCHAR(64) DEFAULT NULL,
    `bankaccounttype_name` VARCHAR(128) DEFAULT NULL,
    `confirmbankaccountnumber` VARCHAR(128) DEFAULT NULL,
    `joinaccount_status` VARCHAR(128) DEFAULT NULL,
    `joinaccount_name` VARCHAR(128) DEFAULT NULL,
    `chequebook_status` VARCHAR(128) DEFAULT NULL,
    `disbursement_accountstatus` CHAR(3) DEFAULT NULL,
    PRIMARY KEY (`institution2bankdtl_gid`)
); 
insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval, sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code, delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date) 
values('SSQM2022072751', 'I2BD', 'institution2bankdtl_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

CREATE TABLE `agr_mst_tinstitution2bankdtlupdatelog` (
    `institution2bankdtlupdatelog_gid` VARCHAR(45) NOT NULL,
    `institution2bankdtl_gid` VARCHAR(45) DEFAULT NULL,
    `institution_gid` VARCHAR(45) DEFAULT NULL,
    `application_gid` VARCHAR(45) DEFAULT NULL,
    `bankaccount_number` VARCHAR(64) DEFAULT NULL,
    `ifsc_code` VARCHAR(45) DEFAULT NULL,
    `bank_name` VARCHAR(256) DEFAULT NULL,
    `branch_name` VARCHAR(256) DEFAULT NULL,
    `micr_code` VARCHAR(45) DEFAULT NULL,
    `accountholder_name` VARCHAR(50) DEFAULT NULL,
    `accounttype_gid` VARCHAR(45) DEFAULT NULL,
    `accounttype_name` VARCHAR(45) DEFAULT NULL,
    `joint_account` CHAR(1) DEFAULT NULL,
    `jointaccountholder_name` VARCHAR(50) DEFAULT NULL,
    `chequebookfacility_available` CHAR(1) DEFAULT NULL,
    `accountopen_date` DATETIME DEFAULT NULL,
    `created_by` VARCHAR(45) DEFAULT NULL,
    `created_date` DATETIME DEFAULT NULL,
    `updated_by` VARCHAR(45) DEFAULT NULL,
    `updated_date` DATETIME DEFAULT NULL,
    `bank_address` LONGTEXT,
    `bankaccount_name` VARCHAR(128) DEFAULT NULL,
    `bankaccounttype_gid` VARCHAR(64) DEFAULT NULL,
    `bankaccounttype_name` VARCHAR(128) DEFAULT NULL,
    `confirmbankaccountnumber` VARCHAR(128) DEFAULT NULL,
    `joinaccount_status` VARCHAR(128) DEFAULT NULL,
    `joinaccount_name` VARCHAR(128) DEFAULT NULL,
    `chequebook_status` VARCHAR(128) DEFAULT NULL,
    `disbursement_accountstatus` CHAR(3) DEFAULT NULL,
    PRIMARY KEY (`institution2bankdtlupdatelog_gid`)
); 
insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval, sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code, delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date) 
values('SSQM2022072752', 'I2BU', 'institution2bankdtlupdatelog_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

/* ---------------End - Task Name -   Bank details Multiple Add at Company Info page 2022-08-02 11:48--------------- */

/* ---------------Start - Task Name -  Trade tab warehouse Increment Field  2022-08-03 11:48--------------- */
create table agr_mst_tapplicationtrade2warehouse (
`applicationtrade2warehouse_gid` varchar(45) NOT NULL,
`application2trade_gid` varchar(45) default null,
`application2loan_gid` varchar(45) default null,
`application_gid` varchar(45) default null,
`creditor_gid` varchar(45) default null,
`warehouse_gid` varchar(45) default null,
`warehouse_agency` varchar(64) default null,
`warehouse_name` varchar(256) default null,
`typeofwarehouse_name` varchar(256) default null,
`volume_uomgid` varchar(45) default null,
`volume_uom` varchar(64) default null,
`totalcapacity_volume` varchar(64) default null,
`totalcapacity_area` varchar(64) default null,
`totalcapacityarea_uomgid` varchar(45) default null,
`area_uom` varchar(64) default null,
`warehouse2address_gid` varchar(45) default null,
`warehouse_address` varchar(500) default null,
`capacity_commodity` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
`capacity_panina` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
`created_by` varchar(45) DEFAULT NULL,
`created_date` datetime DEFAULT NULL,
`updated_by` varchar(45) DEFAULT NULL,
`updated_date` datetime DEFAULT NULL,
PRIMARY KEY (`applicationtrade2warehouse_gid`)
);

insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208030062', 'AT2W', 'applicationtrade2warehouse_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

create table agr_mst_tapplicationtrade2warehouseupdatelog (
`applicationtrade2warehouseupdatelog_gid` varchar(45) NOT NULL,
`applicationtrade2warehouse_gid` varchar(45) default NULL,
`application2trade_gid` varchar(45) default null,
`application2loan_gid` varchar(45) default null,
`application_gid` varchar(45) default null,
`creditor_gid` varchar(45) default null,
`warehouse_gid` varchar(45) default null,
`warehouse_agency` varchar(64) default null,
`warehouse_name` varchar(256) default null,
`typeofwarehouse_name` varchar(256) default null,
`volume_uomgid` varchar(45) default null,
`volume_uom` varchar(64) default null,
`totalcapacity_volume` varchar(64) default null,
`totalcapacity_area` varchar(64) default null,
`totalcapacityarea_uomgid` varchar(45) default null,
`area_uom` varchar(64) default null,
`warehouse2address_gid` varchar(45) default null,
`warehouse_address` varchar(500) default null,
`capacity_commodity` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
`capacity_panina` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
`created_by` varchar(45) DEFAULT NULL,
`created_date` datetime DEFAULT NULL,
`updated_by` varchar(45) DEFAULT NULL,
`updated_date` datetime DEFAULT NULL,
PRIMARY KEY (`applicationtrade2warehouseupdatelog_gid`)
);

insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208030063', 'ATWU', 'applicationtrade2warehouseupdatelog_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);
/* ---------------End - Task Name -  Trade tab warehouse Increment Field  2022-08-03 11:48--------------- */

/* ---------------Start - Task Name -   Scope of insurance field in trade tab 2022-08-03 11:48--------------- */

alter table agr_mst_tapplication2trade add column scopeof_insurancegid varchar(45) default null;

alter table agr_mst_tapplication2trade add column scopeof_insurance varchar(256) default null;
/* ---------------End  - Task Name -    Scope of insurance field in trade tab 2022-08-03 11:48--------------- */

/* --------------Start  - Task Name -    product samagro 2022-08-06 17:23--------------- */
INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,
`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,
`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`) VALUES ('PRD','$','PRD',60,'',1,'Product SamAgro','1','',
'PRD','E1','PRD','','','','N','Y',NULL,NULL,NULL,NULL,3,'Y','','');

INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,
`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,
`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`) VALUES ('PRDESK','PRD','PRDESK',1,'',2,
'Prdouct Desk','1','','Prdouct Desk','','Prdouct Desk','','','','N','N',NULL,NULL,NULL,NULL,3,'Y',' ','');

INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,
`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,
`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`) VALUES ('PRDESKMYA','PRDESK','PRDESKMYA',1,'',3,
'My Assignment','1','','My Assignment','','My Assignment','','','','N','N',NULL,NULL,NULL,NULL,3,'Y','app.AgrMstPrdcPendingAssignmentSummary','');

INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,
`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,
`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`) VALUES ('PRDESKASS','PRDESK','PRDESKASS',1,'',3,
'Assignment','1','','Assignment','','Assignment','','','','N','N',NULL,NULL,NULL,NULL,3,'Y','app.AgrMstProductPendingAssignmentSummary','');

INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,
`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,
`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`) VALUES ('PRDESKPRA','PRDESK','PRDESKPRA',2,'',3,
'Product Approval','1','','Product Approval','','Product Approval','','','','N','N',NULL,NULL,NULL,NULL,3,'Y','app.AgrMstPrdcDescApprovalSummary','');

update adm_mst_tmodule set display_order='51' where module_gid='AGR' and module_gid_parent='$';

update adm_mst_tmodule set display_order='55' where module_gid='AGC' and module_gid_parent='$';

update adm_mst_tmodule set display_order='56' where module_gid='AGD' and module_gid_parent='$';
/* ---------------End  - Task Name -    product samagro 2022-08-06 17:23--------------- */

/* ---------------Start  - Task Name - Product Desk - Samagro 2022-08-08 17:23--------------- */

insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208080001', 'APAG', 'appproductapproval_gid', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

create table agr_trn_tappproductapproval(
appproductapproval_gid varchar(45) primary key,
application_gid varchar(45) default null,
productdesk_gid varchar(45) default null,
product_gid varchar(45) default null,
product_name varchar(64) default null,
product_managergid varchar(45) default null,
product_managername varchar(64) default null,
productmanager_approvalflag char(1) default 'N',
product_membergid varchar(45) default null,
product_membername varchar(64) default null,
productmember_approvalflag char(1) default 'N',
created_by varchar(45) default null,
created_date datetime default null
);

alter table agr_mst_tapplication
add column productdesk_flag char(5) default null;  



alter table agr_mst_tproductdeskmapping
add column app_productdesk char(1) default 'N';

alter table agr_mst_tapplication
add column productdesk_gid varchar(45) default null;

alter table agr_trn_tappproductapproval
add column  productdesk_name varchar(246) default null after productdesk_gid;

alter table agr_trn_tappproductapproval
add column assign_remarks text default null after productmember_approvalflag;

create table agr_trn_tappproductapprovalreassignlog(
appproductreassignlog_gid int NOT NULL AUTO_INCREMENT primary key,
application_gid varchar(45) default null,
appproductapproval_gid varchar(45) default null,  
product_managergid varchar(45) default null,
product_managername varchar(64) default null,
reassignto_product_managergid varchar(45) default null,
reassignto_product_managername varchar(64) default null,
product_membergid varchar(45) default null,
product_membername varchar(64) default null,
reassignto_product_membergid varchar(45) default null,
reassignto_product_membername varchar(64) default null,
assign_remarks text default null,
reassignto_assign_remarks text default null,
created_by varchar(45) default null,
created_date datetime default null
);

alter table agr_trn_tappproductapproval
add column productmanager_approvaldate datetime default null after productmanager_approvalflag,
add column productmember_approvaldate datetime default null after productmember_approvalflag;

alter table agr_trn_tappproductapproval
add column productmanager_approvalremarks text default null after productmanager_approvaldate;

CREATE TABLE `agr_trn_tapplicationproductdescquery` (
`appproductquery_gid` varchar(45) NOT NULL,
`appproductapproval_gid` varchar(45) DEFAULT NULL,
`application_gid` varchar(45) DEFAULT NULL,
`query_title`varchar(250) default NULL,
`query_description` longtext DEFAULT NULL,
`query_status` varchar(45) DEFAULT 'Open',
`closed_date` datetime DEFAULT NULL,
`close_remarks` longtext DEFAULT NULL,
`closed_by`  varchar(45) DEFAULT NULL,
`queryraised_to`  varchar(45) DEFAULT NULL,
`created_by` varchar(45) DEFAULT NULL,
`created_date` datetime DEFAULT NULL,
PRIMARY KEY (`appproductquery_gid`)
);

INSERT INTO `adm_mst_tsequence` (`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,
`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`) 
VALUES ('SSQM202208090001','APDQ','appproductquery_gid','4','8','N','N','N','N','N','N','','','','2022','N','',NULL,'',NULL);

alter table agr_mst_tapplication Add productquery_status varchar(45) Default 'Pending';
/* ---------------End  - Task Name -    Product Desk - Samagro 2022-08-08 17:23--------------- */



/* ---------------Start  - Task Name - Customer Onboarding & Approval 2022-08-12 12:23--------------- */
INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`)
VALUES ('AGRMSTCOB','AGRMST','AGRMSTCOB',3,' ',3,'Customer Onboarding','2',' ','Customer Onboarding','','Customer Onboarding','','','','N','N','',NULL,'',NULL,3,'Y','app.AgrMstCustomerOnboardingSummary','');



INSERT INTO `adm_mst_tmodule` (`module_gid`,`module_gid_parent`,`module_code`,`display_order`,`module_link`,`menu_level`,`module_name`,`status`,`image_url`,`group_type`,`modulemanager_gid`,`breadcrumb_name`,`approval_flag`,`approval_tablename`,`approval_type`,`approval_limit`,`module_flag`,`created_by`,`created_date`,`updated_by`,`updated_date`,`max_menulevel`,`lw_flag`,`sref`,`icon`)
VALUES ('AGRMSTCUA','AGRMST','AGRMSTCUA',4,' ',3,'Customer Approval','2',' ','Customer Approval','','Customer Approval','','','','N','N','',NULL,'',NULL,3,'Y','app.AgrMstCustomerApprovalSummary','');

#Buyer

create table agr_mst_tbyronboard2product (PRIMARY KEY(`application2product_gid`)) as select * from agr_mst_tapplication2product;

create table agr_mst_tbyronboard2contactno (PRIMARY KEY(`application2contact_gid`)) as select * from agr_mst_tapplication2contactno;

create table agr_mst_tbyronboard2email (PRIMARY KEY(`application2email_gid`)) as select * from agr_mst_tapplication2email;

create table agr_mst_tbyronboard2geneticcode (PRIMARY KEY(`application2geneticcode_gid`)) as select * from agr_mst_tapplication2geneticcode;

create table agr_mst_tbyronboard (PRIMARY KEY(`application_gid`)) as select * from agr_mst_tapplication;

create table agr_mst_tbyronboardinstitution2branch (PRIMARY KEY(`institution2branch_gid`)) as select * from agr_mst_tinstitution2branch;

create table agr_mst_tbyronboardinstitution2ratingdetail (PRIMARY KEY(`institution2ratingdetail_gid`)) as select * from agr_mst_tinstitution2ratingdetail;

create table agr_mst_tbyronboardinstitution2mobileno (PRIMARY KEY(`institution2mobileno_gid`)) as select * from agr_mst_tinstitution2mobileno;

create table agr_mst_tbyronboardinstitution2bankdtl (PRIMARY KEY(`institution2bankdtl_gid`)) as select * from agr_mst_tinstitution2bankdtl;

create table agr_mst_tbyronboardinstitution2documentupload (PRIMARY KEY(`institution2documentupload_gid`)) as select * from agr_mst_tinstitution2documentupload;

create table agr_mst_tbyronboardinstitution2licensedtl (PRIMARY KEY(`institution2licensedtl_gid`)) as select * from agr_mst_tinstitution2licensedtl;

create table agr_mst_tbyronboard2institution (PRIMARY KEY(`institution_gid`),  INDEX(`application_gid`)) as select * from agr_mst_tinstitution;

create table agr_mst_tbyronboardcontact2mobileno (PRIMARY KEY(`contact2mobileno_gid`)) as select * from agr_mst_tcontact2mobileno;

create table agr_mst_tbyronboardcontact2idproof (PRIMARY KEY(`contact2idproof_gid`)) as select * from agr_mst_tcontact2idproof;

create table agr_mst_tbyronboardcontact2address (PRIMARY KEY(`contact2address_gid`)) as select * from agr_mst_tcontact2address;

create table agr_mst_tbyronboardcontact2document (PRIMARY KEY(`contact2document_gid`)) as select * from agr_mst_tcontact2document;

create table agr_mst_tbyronboardcontact (PRIMARY KEY(`contact_gid`), INDEX(`application_gid`)) as select * from agr_mst_tcontact;

#Supplier

create table agr_mst_tsupronboard2product (PRIMARY KEY(`application2product_gid`)) as select * from agr_mst_tsuprapplication2product;

create table agr_mst_tsupronboard2contactno (PRIMARY KEY(`application2contact_gid`)) as select * from agr_mst_tsuprapplication2contactno;

create table agr_mst_tsupronboard2email (PRIMARY KEY(`application2email_gid`)) as select * from agr_mst_tsuprapplication2email;

create table agr_mst_tsupronboard2geneticcode (PRIMARY KEY(`application2geneticcode_gid`)) as select * from agr_mst_tsuprapplication2geneticcode;

create table agr_mst_tsupronboard (PRIMARY KEY(`application_gid`)) as select * from agr_mst_tsuprapplication;

create table agr_mst_tsupronboardinstitution2branch (PRIMARY KEY(`institution2branch_gid`)) as select * from agr_mst_tsuprinstitution2branch;

create table agr_mst_tsupronboardinstitution2ratingdetail (PRIMARY KEY(`institution2ratingdetail_gid`)) as select * from agr_mst_tsuprinstitution2ratingdetail;

create table agr_mst_tsupronboardinstitution2mobileno (PRIMARY KEY(`institution2mobileno_gid`)) as select * from agr_mst_tsuprinstitution2mobileno;

create table agr_mst_tsupronboardinstitution2documentupload (PRIMARY KEY(`institution2documentupload_gid`)) as select * from agr_mst_tsuprinstitution2documentupload;

create table agr_mst_tsupronboardinstitution2licensedtl (PRIMARY KEY(`institution2licensedtl_gid`)) as select * from agr_mst_tsuprinstitution2licensedtl;

create table agr_mst_tsupronboard2institution (PRIMARY KEY(`institution_gid`),  INDEX(`application_gid`)) as select * from agr_mst_tsuprinstitution;

create table agr_mst_tsupronboardcontact2mobileno (PRIMARY KEY(`contact2mobileno_gid`)) as select * from agr_mst_tsuprcontact2mobileno;

create table agr_mst_tsupronboardcontact2idproof (PRIMARY KEY(`contact2idproof_gid`)) as select * from agr_mst_tsuprcontact2idproof;

create table agr_mst_tsupronboardcontact2address (PRIMARY KEY(`contact2address_gid`)) as select * from agr_mst_tsuprcontact2address;

create table agr_mst_tsupronboardcontact2document (PRIMARY KEY(`contact2document_gid`)) as select * from agr_mst_tsuprcontact2document;

create table agr_mst_tsupronboardcontact (PRIMARY KEY(`contact_gid`), INDEX(`application_gid`)) as select * from agr_mst_tsuprcontact;

create table agr_mst_tbyronboardinstitution2address (PRIMARY KEY(`institution2address_gid`)) as select * from agr_mst_tinstitution2address;
create table agr_mst_tbyronboardcontact2email (PRIMARY KEY(`contact2email_gid`)) as select * from agr_mst_tcontact2email;
create table agr_mst_tsupronboardinstitution2address (PRIMARY KEY(`institution2address_gid`)) as select * from agr_mst_tsuprinstitution2address;
create table agr_mst_tsupronboardcontact2email (PRIMARY KEY(`contact2email_gid`)) as select * from agr_mst_tsuprcontact2email;

alter table agr_mst_tbyronboard
add column onboard_approvalstatus char(1) default 'N',
add column onboard_approvedby varchar(45) default null,
add column onboard_approveddate datetime default null;

alter table agr_mst_tsupronboard
add column onboard_approvalstatus char(1) default 'N',
add column onboard_approvedby varchar(45) default null,
add column onboard_approveddate datetime default null;

alter table agr_mst_tbyronboard
add column onboard_approvedbyname varchar(64) default null after onboard_approvedby;

alter table agr_mst_tsupronboard
add column onboard_approvedbyname varchar(64) default null after onboard_approvedby;

alter table agr_mst_tbyronboard
add column  createdby_name varchar(64) default null after created_date;

alter table agr_mst_tsupronboard
add column  createdby_name varchar(64) default null after created_date;
alter table agr_mst_tbyronboard
add column  onboard_approvalremarks varchar(64) default null after onboard_approvalstatus;


alter table agr_mst_tsupronboard
add column  onboard_approvalremarks varchar(64) default null after onboard_approvalstatus;

create table agr_mst_tbyronboardcontact2panabsencereason as select * from agr_mst_tcontact2panabsencereason;
create table agr_mst_tbyronboardcontact2panform60 as select * from agr_mst_tcontact2panform60;
create table agr_mst_tbyronboardinstitution2email as select * from agr_mst_tinstitution2email;
create table agr_mst_tsupronboardcontact2panabsencereason as select * from agr_mst_tcontact2panabsencereason;
create table agr_mst_tsupronboardcontact2panform60 as select * from agr_mst_tcontact2panform60;
create table agr_mst_tsupronboardinstitution2email as select * from agr_mst_tinstitution2email;

create table agr_mst_tsupronboardinstitution2form60documentupload (PRIMARY KEY(`institution2form60documentupload_gid`)) as select * from agr_mst_tsuprinstitution2form60documentupload;

create table agr_mst_tbyronboardinstitution2form60documentupload (PRIMARY KEY(`institution2form60documentupload_gid`)) as select * from agr_mst_tinstitution2form60documentupload;

alter table agr_mst_tbyronboard
add column approval_submitteddate datetime default null,                                 
add column approval_submittedflag char(1) default 'N';
                                 
alter table agr_mst_tsupronboard
add column approval_submitteddate datetime default null,
add column approval_submittedflag char(1) default 'N';

 insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208190001', 'BYRG', 'buyerapplication_id', '4', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);
/* ---------------End  - Task Name - Customer Onboarding & Approval 2022-08-12 12:23--------------- */

/* ---------------Start  - Task Name - Onboarding Task - Initiate 2022-08-24 --------------- */

alter table agr_mst_tbyronboard
add column initiated_date datetime default null,
add column initiated_remarks longtext default null;



alter table agr_mst_tsupronboard
add column initiated_date datetime default null,
add column initiated_remarks longtext default null;
/* ---------------End  - Task Name - Onboarding Task - Initiate 2022-08-24 --------------- */

/* ---------------Start  - Task Name - Onboarding Edit 2022-08-24 --------------- */
create table agr_mst_tbyronboard2institutionupdateLOG as select * from agr_mst_tinstitutionupdateLOG;
create table agr_mst_tbyronboardcontactupdatelog as select * from  agr_mst_tcontactupdatelog;
create table  agr_mst_tbyronboardcontactupdateLOG as select * from agr_mst_tcontactupdateLOG;
create table agr_mst_tsupronboard2institutionupdateLOG as select * from agr_mst_tsuprinstitutionupdateLOG;
create table agr_mst_tsupronboarcontactupdateLOG as select * from agr_mst_tsuprcontactupdateLOG;
/* ---------------End  - Task Name - Onboarding Edit 2022-08-24 --------------- */

/* ---------------Start  - Task Name - Ref no Generation - Buyer, Supplier,Credio 2022-08-26 --------------- */

insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208260001', 'SURG', 'Supplierapplication_id', '5', '0', 'Y', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);


UPDATE `samunnati`.`adm_mst_tsequence` SET `sequence_format` = '5', `branch_flag` = 'N' WHERE (`sequence_gid` = 'SSQM2022062300025');


UPDATE `samunnati`.`adm_mst_tsequence` SET `sequence_format` = '5',`sequence_flag` = 'Y' WHERE (`sequence_gid` = 'SSQM202208190001');

insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202208260002', 'WARG', 'warehouseref_id', '5', '0', 'Y', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

/* ---------------End  - Task Name - Ref no Generation - Buyer, Supplier,Credio 2022-08-26 --------------- */


/* ---------------start  - Task Name - label Correction  2022-08-29 --------------- */
UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Buyer / Supplier Onboarding',
`group_type` = 'Buyer / Supplier Onboarding', `breadcrumb_name` = 'Buyer / Supplier Onboarding' WHERE (`module_gid` = 'AGRMSTCOB');



UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Buyer / Supplier Approval',
`group_type` = 'Buyer / Supplier Approval', `breadcrumb_name` = 'Buyer / Supplier Approval' WHERE (`module_gid` = 'AGRMSTCUA');



UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Buyer Proposal',
`group_type` = 'Buyer Proposal', `breadcrumb_name` = 'Buyer Proposal' WHERE (`module_gid` = 'AGRMSTACS');



UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Supplier Proposal',
`group_type` = 'Supplier Proposal', `breadcrumb_name` = 'Supplier Proposal' WHERE (`module_gid` = 'AGRMSTSCS');

UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Buyer Business Approval',
`group_type` = 'Buyer Business Approval', `breadcrumb_name` = 'Buyer Business Approval' WHERE (`module_gid` = 'AGRMSTBUA');

UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Other Creditor Master',
`group_type` = 'Other Creditor Master', `breadcrumb_name` = 'Other Creditor Master' WHERE (`module_gid` = 'AGRMSTCMS');
/* ---------------End  - Task Name - label Correction  2022-08-29 --------------- */

/* ---------------Start  - Task Name - Virtual Bank Account Info  2022-09-01 --------------- */
insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202209010003', 'BVAN', 'buyerVirtualaccount_no', '6', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);

 insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202209010004', 'BCIN', 'buyerContract_Id', '6', '0001', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);


alter table agr_mst_tbyronboard
add column virtualaccount_number varchar(45) default null,
add column customerbank_name varchar(64) default null,
add column branch_name varchar(500) default null,
add column ifsc_code varchar(100) default null,
add column micr_code varchar(100) default null;

alter table agr_mst_tapplication
add column contract_id varchar(45) default null;

UPDATE `adm_mst_tsequence` SET `sequence_flag` = 'Y' WHERE (`sequence_gid` = 'SSQM202209010003');

alter table agr_mst_tbyronboardinstitution2bankdtl
add column primary_status char(10) default 'Yes';


alter table agr_mst_tloansubproduct
add column contract_ref varchar(15) default null;


/* ---------------End  - Task Name - Virtual Bank Account Info  2022-09-01 --------------- */


/* ---------------End  - Task Name - Onboard Initiate Functionality  2022-09-08 --------------- */
alter table agr_mst_tapplication
   add column buyeronboard_gid varchar(45) default null;
   
   insert into adm_mst_tsequence(sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval,
sequence_flag, branch_flag, department_flag, year_flag, month_flag, location_flag, company_code,
delimeter, runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202209080001', 'BYOG', 'buyeronboard_gid', '5', '0', 'N', 'N', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', NULL, '', NULL);



create table agr_mst_tonboardinitiatedtl
(onboardinitiatedtl_gid  int NOT NULL AUTO_INCREMENT primary key,
buyeronboard_gid varchar(45) default null,
supplieronboard_gid varchar(45) default null,
application_gid varchar(45) default null,
product_gid varchar(45) default null,
product_name varchar(246) default null,
program_gid varchar(45) default null,program_name varchar(246) default null,  
initiated_remarks longtext default null,
created_byname varchar(64) default null,
created_by varchar(45) default null,created_date  datetime default null
)


/* ---------------End  - Task Name - Onboard Initiate Functionality  2022-09-08 --------------- */

/* ---------------Start  - Task Name - Legal Tag Status  2022-11-01 --------------- */


CREATE TABLE `agr_mst_tonboardlgltagstatuslog` (
   `onboardlgltagstatuslog_gid` int(15) NOT NULL AUTO_INCREMENT,
   `byronboard_gid` varchar(45) DEFAULT NULL,
   `supronboard_gid` varchar(45) DEFAULT NULL,
   `lgltag_status` varchar (15) DEFAULT Null,
   `created_by` varchar(45) DEFAULT NULL,
   `created_date` datetime DEFAULT NULL,
   PRIMARY KEY (`onboardlgltagstatuslog_gid`)
) ;

alter table agr_mst_tbyronboard add column lgltag_status varchar (15) default 'Active';

/* ---------------End  - Task Name - Legal Tag Status  2022-11-01  --------------- */

/* ---------------Start  - Task Name - Task 96: Amendment Master  2022-12-29  --------------- */

INSERT INTO `adm_mst_tsequence` (`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`)
VALUES ('SSQM202212290001','AMDT','amendment_gid','4','001','N','N','N','N','','N','','','','2022','N',NULL,NULL,NULL,NULL);

INSERT INTO `adm_mst_tsequence` (`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`)
VALUES ('SSQM202212290002','AMTL','amendmentlog_gid','4','001','N','N','N','N','','N','','','','2022','N',NULL,NULL,NULL,NULL);

INSERT INTO `adm_mst_tsequence` (`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`)
VALUES ('SSQM202212290003','AMTI','amendmentinactivelog_gid','4','001','N','N','N','N','','N','','','','2022','N',NULL,NULL,NULL,NULL);

CREATE TABLE `agr_mst_tamendment` (
  `amendment_gid` varchar(45) NOT NULL,
  `amendment` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `lms_code` varchar(125) DEFAULT NULL,
  `bureau_code` varchar(125) DEFAULT NULL,
  `status` char(1) DEFAULT 'Y',
  `remarks` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `created_by` varchar(45) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(45) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`amendment_gid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `agr_mst_tamendmentlog` (
  `amendmentlog_gid` varchar(45) NOT NULL,
  `amendment_gid` varchar(45) DEFAULT NULL,
  `amendment` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `updated_by` varchar(45) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`amendmentlog_gid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `agr_mst_tamendmentinactivelog` (
  `amendmentinactivelog_gid` varchar(45) NOT NULL,
  `amendment_gid` varchar(45) DEFAULT NULL,
  `amendment` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `remarks` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `status` char(1) DEFAULT NULL,
  `updated_by` varchar(45) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`amendmentinactivelog_gid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

 

insert into adm_mst_tmodule(module_gid, module_gid_parent, module_code, display_order, module_link, menu_level, module_name, status, image_url, group_type, modulemanager_gid,
breadcrumb_name, approval_flag, approval_tablename, approval_type, approval_limit, module_flag, created_by, created_date, updated_by, updated_date, max_menulevel, lw_flag, sref, icon)
values ('AGRMSTAMT', 'AGRMST', 'AGRMSTAMT', '61', ' ', '3', 'Amendment', '2', ' ', 'Amendment', '', 'Amendment', '', '', '', 'N', 'N', '', NULL, '', NULL, '3', 'Y', 'app.AgrMstAmendmentSummary', '');


/* ---------------End  - Task Name - Task 96: Amendment Master  2022-12-29  --------------- */

/* ---------------Start  - Task Name - Task 95: Warehouse Report and Other Creditor Report 2023-01-02  --------------- */

insert into adm_mst_tmodule(module_gid, module_gid_parent, module_code, display_order, module_link, menu_level, module_name, status, image_url, group_type, modulemanager_gid, 
breadcrumb_name, approval_flag, approval_tablename, approval_type, approval_limit, module_flag, created_by, created_date, updated_by, updated_date, max_menulevel, lw_flag, sref, icon)
values ('AGRRPTWRH', 'AGRRPT', 'AGRRPTWRH', '57', ' ', '3', 'WareHouse Report', '2', ' ', 'WareHouse Report', '', 'WareHouse Report', '', '', '', 'N', 'N', '', NULL, '', NULL, '3', 'Y', 'app.AgrMstWarehouseReport', '');

insert into adm_mst_tmodule(module_gid, module_gid_parent, module_code, display_order, module_link, menu_level, module_name, status, image_url, group_type, modulemanager_gid, 
breadcrumb_name, approval_flag, approval_tablename, approval_type, approval_limit, module_flag, created_by, created_date, updated_by, updated_date, max_menulevel, lw_flag, sref, icon)
values ('AGRRPTOCR', 'AGRRPT', 'AGRRPTOCR', '58', ' ', '3', 'Other Creditor Report', '2', ' ', 'Other Creditor Report', '', 'Other Creditor Report', '', '', '', 'N', 'N', '', NULL, '', NULL, '3', 'Y', 'app.AgrMstOtherCreditorReport', '');

/* ---------------End  - Task Name - Task 95: Warehouse Report and Other Creditor Report 2023-01-03  --------------- */

/* ---------------Start  - Task Name - Task 80 : Amendment - For buyer 2023-01-03  --------------- */
 
CREATE TABLE `agr_mst_tamendmentreason` (
  `amendmentreason_gid` varchar(45) NOT NULL,
  `application_gid`  varchar(45) NOT NULL,
  `buyeronboard_gid` varchar(45) NOT NULL,
  `amendment_gid` varchar(45) NOT NULL,
  `amendment` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `created_by` varchar(45) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(45) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`amendmentreason_gid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

 

insert into adm_mst_tsequence (sequence_gid, sequence_code, sequence_name, sequence_format, sequence_curval, sequence_flag,
branch_flag, department_flag, year_flag, month_flag, location_flag, company_code, delimeter,
runningno_prefix, finyear, carry_forward, created_by, created_date, updated_by, updated_date)
values('SSQM202301030001', 'AMTR', 'amendmentreason_gid', '4', '0001', 'N', 'Y', 'N', 'N', 'N', 'N', '', '', '', '2022', 'N', '', null, '', null);

/* ---------------End  - Task Name - Task 80 : Amendment - For buyer 2023-01-03  --------------- */

/* ---------------Start- Task - error log - 11/01/2023 --------------- */

Alter table agr_mst_tbyronboard2product add column `graceperiod_days` varchar(10) DEFAULT NULL;

/* ---------------End- Task - error log - 11/01/2023 --------------- */


/* ---------------Start- Task 89: SamAgro - PMG document checklist, Scanned document changes - 19/01/2023 --------------- */

CREATE TABLE `agr_trn_tnotcleardocchecklist` (
   `notcleardocchecklist_gid` int(45) NOT NULL AUTO_INCREMENT,
   `documentcheckdtl_gid` varchar(75) DEFAULT NULL,
   `mstchecklist_gid` varchar(75) DEFAULT NULL,
   `checklist_name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
   `created_by` varchar(75) DEFAULT NULL,
   `created_date` datetime DEFAULT NULL,
   `fromphysical_document` char(1) DEFAULT 'N',
   PRIMARY KEY (`notcleardocchecklist_gid`)
) ENGINE=InnoDB AUTO_INCREMENT=01 DEFAULT CHARSET=latin1 ;

CREATE TABLE `agr_trn_tpmgscannedphysicalstatusupdatelog` (
   `pmgscannedphysicalstatusupdatelog_gid` int(45) NOT NULL AUTO_INCREMENT,
   `application_gid` varchar(45) DEFAULT NULL,
   `groupdocumentchecklist_gid` varchar(45) DEFAULT NULL,
   `covenant_type` char(2) DEFAULT NULL,
   `scanneddoc_flag` char(2) DEFAULT NULL,
   `status_update` varchar(45) DEFAULT NULL,
   `created_by` varchar(45) DEFAULT NULL,
   `created_date` datetime DEFAULT NULL,
   `fromphysical_document` char(1) DEFAULT 'N',
   PRIMARY KEY (`pmgscannedphysicalstatusupdatelog_gid`)
) ENGINE=InnoDB AUTO_INCREMENT=01 DEFAULT CHARSET=latin1;

alter table agr_trn_ttagquery  
add column   `deferralchecklist_gid` varchar(45) DEFAULT NULL,
add column   `deferralchecklist_name` varchar(245) DEFAULT NULL,
add column   `query_code` varchar(45) DEFAULT NULL,
add column   `document_gid` varchar(45) DEFAULT NULL,
add column   `document_name` varchar(200) DEFAULT NULL,
add column   `queryclosed_status` varchar(45) DEFAULT NULL,
add column   `querystatus_updatedby` varchar(45) DEFAULT NULL,
add column   `querystatus_updateddate` datetime DEFAULT NULL;

alter table agr_trn_tscanneddocument
add column  `document_code` varchar(45) DEFAULT NULL,
add column  `tagquery_gid` varchar(45) DEFAULT NULL,
add column  `scanneddocument_code` varchar(45) DEFAULT NULL,
add column  `replacementdocument_code` varchar(45) DEFAULT NULL;

alter table agr_trn_tgroupcovenantdocumentchecklist add column  `document_code` varchar(45) DEFAULT NULL,
add column  `documentsubmission_date` date DEFAULT NULL,
  add column `documentsubmission_updatedby` varchar(45) DEFAULT NULL,
  add column `documentsubmission_updateddate` datetime DEFAULT NULL,
  add column `physicaldocumentsubmission_date` date DEFAULT NULL,
  add column `physicaldocumentsubmission_updatedby` varchar(45) DEFAULT NULL,
  add column `physicaldocumentsubmission_updateddate` datetime DEFAULT NULL,
  add column `physical_confirmation_remarks` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci; alter table agr_trn_tgroupdocumentchecklist add column  `document_code` varchar(45) DEFAULT NULL,
add column  `documentsubmission_date` date DEFAULT NULL,
  add column `documentsubmission_updatedby` varchar(45) DEFAULT NULL,
  add column `documentsubmission_updateddate` datetime DEFAULT NULL,
  add column `physicaldocumentsubmission_date` date DEFAULT NULL,
  add column `physicaldocumentsubmission_updatedby` varchar(45) DEFAULT NULL,
  add column `physicaldocumentsubmission_updateddate` datetime DEFAULT NULL,
  add column `physical_confirmation_remarks` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

alter table agr_trn_tinitiateextendorwaiver 
add column   `due_date` varchar(45) DEFAULT NULL,
  add column   `approval_initiation` char(1) DEFAULT 'N',
  add column   `approval_initiationgid` varchar(45) DEFAULT NULL,
  add column   `approval_initiatedby` varchar(45) DEFAULT NULL,
  add column   `approval_initiateddate` datetime DEFAULT NULL;

alter table agr_trn_textendorwaiverapproval add column `approval_person` varchar(45) DEFAULT NULL; 
alter table agr_trn_tdeferraltagdoc add column `initiateextendorwaiver_gid` varchar(45) DEFAULT NULL;

UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Softcopy Vetting', `group_type` = 'Softcopy Vetting', `breadcrumb_name` = 'Softcopy Vetting' WHERE (`module_gid` = 'AGDMGTDTS');
UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Softcopy Vetting Followup', `group_type` = 'Softcopy Vetting Followup', `breadcrumb_name` = 'Softcopy Vetting Followup' WHERE (`module_gid` = 'AGDMGTSDF');
UPDATE `samunnati`.`adm_mst_tmodule` SET `module_name` = 'Softcopy Vetting', `group_type` = 'Softcopy Vetting', `breadcrumb_name` = 'Softcopy Vetting' WHERE (`module_gid` = 'AGDSMPSSD'); INSERT INTO `adm_mst_tsequence`
(`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`) 
VALUES ('SSQM202201300001','SCVC','scanneddocumentcode_gid ','5','0','Y','N','N','N','N','N','','','','2022','Y','',NULL,'',NULL); INSERT INTO `adm_mst_tsequence` 
(`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`) 
VALUES ('SSQM202201300002','PMGQ','pmgquerycode','4','0','Y','N','N','N','N','N','','','','2022','Y','',NULL,'',NULL);

alter table agr_mst_tcreditor2cheque  add column  primary_status char(10) default null;

alter table agr_trn_tcovanantdocumentcheckdtls add column `overall_docstatus` varchar(100) DEFAULT NULL;
/* ---------------End- Task 89: SamAgro - PMG document checklist, Scanned document changes - 19/01/2023 --------------- */

/* ---------------Start - Task 119: Old ARN overall validity expiry date - 03/02/2023 --------------- */
create table agr_mst_tonboardclonelog(
onboardclonelog_gid int(11) primary key AUTO_INCREMENT,
onboard_gid varchar (45) default null,
existingapplication_gid varchar(45) default null,
cloneapplication_gid varchar(45) default null,
clone_status varchar(20) default null,
existing_overallvaliditydate datetime default null,
existing_overallcalculation varchar(100) default null,
latest_overallvaliditydate datetime default null,
latest_overallcalculation varchar(100) default null,
created_by varchar(45) default null,
created_date datetime default null
);
/* ---------------End - Task 119: Old ARN overall validity expiry date - 03/02/2023 --------------- */

/* ---------------Start - Task 125: Raise query Concept in Onboarding Approval to onboarding creation (Buyer & Supplier) - 06/02/2023 --------------- */
INSERT INTO `adm_mst_tsequence` 
(`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`)
VALUES ('SSQM202202020001','BSQR','onboardquery_gid','4','0','N','N','N','N','N','N','','','','2022','N','',NULL,'',NULL);

 CREATE TABLE `agr_mst_tonboardquery` (
`onboardquery_gid` varchar(45) NOT NULL,
`byronboard_gid` varchar(45) DEFAULT NULL,
`supronboard_gid` varchar(45) DEFAULT NULL,
`query_title` varchar(125) DEFAULT NULL,
`query_description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
`query_status` varchar(10) DEFAULT 'Open',
`created_by` varchar(45) DEFAULT NULL,
`created_date` datetime DEFAULT NULL,
`close_remarks` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
`closed_by` varchar(45) DEFAULT NULL,
`closed_date` datetime DEFAULT NULL,
PRIMARY KEY (`onboardquery_gid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

alter table agr_mst_tbyronboard add column query_status varchar (15) default null; 
alter table agr_mst_tsupronboard add column query_status varchar (15) default null;

/* ---------------End - Task 125: Raise query Concept in Onboarding Approval to onboarding creation (Buyer & Supplier) - 06/02/2023 --------------- */

/* ---------------Start - Task 129: Visit report code generation  - Buyer & Supplier - 13/02/2023 --------------- */
INSERT INTO `adm_mst_tsequence`
(`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`)
VALUES ('SSQM202302130001','VRRM','Agro Visit Report Rm Code','4','0','Y','N','N','N','N','N','','','VRRM','2022','N','',NULL,'',NULL);
INSERT INTO `adm_mst_tsequence`
(`sequence_gid`,`sequence_code`,`sequence_name`,`sequence_format`,`sequence_curval`,`sequence_flag`,`branch_flag`,`department_flag`,`year_flag`,`month_flag`,`location_flag`,`company_code`,`delimeter`,`runningno_prefix`,`finyear`,`carry_forward`,`created_by`,`created_date`,`updated_by`,`updated_date`)
VALUES ('SSQM202302130002','VRCM','Agro Visit Report Cm Code','4','0','Y','N','N','N','N','N','','','VRCM','2022','N','',NULL,'',NULL);
alter table agr_mst_tapplicationvisitreport add column visitreport_id    varchar(45) default null;
alter table agr_mst_tsuprapplicationvisitreport add column visitreport_id    varchar(45) default null;
/* ---------------End - Task 129: Visit report code generation  - Buyer & Supplier - 13/02/2023 --------------- */

/* ---------------Start - Bug : CC meeting title Data long issue - Send back - 21/02/2023 --------------- */
alter table agr_mst_tccschedulemeetinglog change ccmeeting_title ccmeeting_title varchar(256) default null;
/* ---------------End - Bug : CC meeting title Data long issue - Send back - 21/02/2023 --------------- */


/* ---------------Start - Task 140: Buyer Shortclose condition change 27/02/2023 --------------- */
ALTER TABLE agr_mst_tapplication ADD COLUMN `expired_flag` char(1) DEFAULT 'Y';
/* ---------------End - Task 140: Buyer Shortclose condition change 27/02/2023 --------------- */

/* ---------------Start - Task 150: Apply SP in PMG Pending & Accepted Summary [Buyer & Supplier] -SP- 01/03/2023 --------------- */

DELIMITER $$
USE `samunnati`$$
CREATE PROCEDURE `agr_trn_pmgpendingapplicationsummary` ()
BEGIN
select a.application_gid,a.application_no,a.customerref_name,
a.customer_urn, a.creditgroup_name, a.customer_name as customer_name, 
date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, 
concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as updated_by,  
a.creditgroup_gid,a.product_gid,a.variety_gid,a.renewal_flag,a.amendment_flag,
a.shortclosing_flag, ccgroup_name
from agr_mst_tapplication a  
left join hrm_mst_temployee b on b.employee_gid = a.updated_by  
left join adm_mst_tuser c on c.user_gid = b.user_gid 
where a.approval_status = 'CC Approved' and a.process_type is null and a.close_flag ='N'
group by a.application_gid order by a.updated_date desc  ;
END$$

DELIMITER ;

DELIMITER $$
USE `samunnati`$$
CREATE PROCEDURE `agr_trn_suprpmgacceptedapplicationsummary` ()
BEGIN
select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, 
a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, 
concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, a.ccgroup_name, group_concat(e.ccadmin_name),
date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, a.approval_status,
a.creditgroup_gid, d.cadgroup_name from agr_mst_tsuprapplication a 
left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by 
left join adm_mst_tuser c on c.user_gid = b.user_gid 
left join agr_trn_tsuprprocesstype_assign d on d.application_gid = a.application_gid 
left join agr_mst_tsuprccschedulemeeting e on e.application_gid = a.application_gid
where a.process_type = 'Accept' 
group by a.application_gid order by a.updated_date desc; 
END$$

DELIMITER ;


DELIMITER $$
USE `samunnati`$$
CREATE PROCEDURE `agr_trn_suprpmgpendingapplicationsummary` ()
BEGIN
select a.application_gid,a.application_no,a.customerref_name,
a.customer_urn, a.creditgroup_name, a.customer_name as customer_name, 
date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status, 
concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as updated_by,  
a.creditgroup_gid,a.product_gid,a.variety_gid, ccgroup_name
from agr_mst_tsuprapplication a  
left join hrm_mst_temployee b on b.employee_gid = a.updated_by  
left join adm_mst_tuser c on c.user_gid = b.user_gid 
where a.approval_status = 'CC Approved' and a.process_type is null 
group by a.application_gid order by a.updated_date desc  ;
END$$

DELIMITER ;

DELIMITER $$
USE `samunnati`$$
CREATE PROCEDURE `agr_trn_pmgacceptedapplicationsummary` ()
BEGIN
select a.application_gid,a.contract_id,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, 
a.customer_name as customer_name, date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, 
concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, a.ccgroup_name, group_concat(e.ccadmin_name),
date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by, a.approval_status,
a.creditgroup_gid, d.cadgroup_name,a.renewal_flag,a.amendment_flag,a.shortclosing_flag from agr_mst_tapplication a 
left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by 
left join adm_mst_tuser c on c.user_gid = b.user_gid 
left join agr_trn_tprocesstype_assign d on d.application_gid = a.application_gid 
left join agr_mst_tccschedulemeeting e on e.application_gid = a.application_gid
where a.process_type = 'Accept' 
group by a.application_gid order by a.updated_date desc; 
END$$

DELIMITER ;



/* ---------------End - Task 150: Apply SP in PMG Pending & Accepted Summary [Buyer & Supplier] -SP- 01/03/2023 --------------- */

