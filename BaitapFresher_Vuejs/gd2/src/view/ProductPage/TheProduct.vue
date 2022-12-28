<template>
    <div class="Productcontainer">
    <div class="product-content-top">
      <div class="product-content-top-left">
        <div class="product-content-top-left-label">
          <label class="product-content-top-label">Danh sách hàng hóa, dịch vụ</label>
          <div class="product-content-top-underlabel">
              <div class="product-content-top-underlabel-icon"></div>
              <label>Tất cả danh mục</label>
          </div>  
        </div>
      </div>
      <div class="product-content-top-right">
        <div class="product-guid">
          <div class="product-guid-icon"></div>
          <a href="#">Hướng dẫn</a>
        </div>
        <MButton :ButtonCss="'btn-button-utilities'" :text="'Tiện ích'"/>
        <MButton :ButtonCss="'btn-button-product'" :text="'Nhóm hàng hóa, dịch vụ'"/>
        <div class="btn-button-openPopupSelect" @click="openPopupSelect(null)">
          <MButton  :ButtonCss="'btn-button-openPopup'" :text="'Thêm'"/>
        </div>
      </div>
    </div>
    <div class="product-overview" v-show="isShowOverview">
        <div class="product-overview-ri">
          <div class="product-overview-ri-icon">
            <span class="product-tooltip">Bấm vào để lọc</span>
          </div>
          <div class="product-overview-ri-left">
            <div class="product-overview-ri-left-top">{{FilterStatus_out_stock}}</div>
            <div class="product-overview-ri-left-center">Hàng hóa</div>
            <div class="product-overview-ri-left-bottom">SẮP HẾT HÀNG</div>
          </div>
        </div>
        <div class="product-overview-le">
           <div class="product-overview-le-icon">
             <span class="product-tooltip">Bấm vào để lọc</span>
           </div>
          <div class="product-overview-le-left">
            <div class="product-overview-le-left-top">{{FilterStatus_not_exist}}</div>
            <div class="product-overview-ri-left-center">Hàng hóa</div>
            <div class="product-overview-ri-left-bottom">HẾT HÀNG</div>
          </div>
          <div class="product-overview-reload"></div>
        </div>
      </div>
    <div class="content-table" :style="TableStyle">
      <div class="product-content-toolbar">
        <div class="product-content-toolbar-left">
          <div class="product-content-toolbar-left-icon"></div>
          <div class="product-content-toolbar-left-btn"  @click="openDropDelete">
            <MButton :ButtonCss="'btn-button-actionMutile'" :text="'Thực hiện hàng loạt'" :iconcss="'icon-filter'" /></div>
          <div class="product-content-toolbar-left-btn" @click="showFilter(1)" ><MButton :ButtonCss="'btn-button-filter'" :iconcss="'icon-filter'" :text="'Lọc'"/></div>
          <div class="toolbar-filter-label" v-for="(item, index) in BindingFilterValue" :key="item">
            <label class="label-filter" >{{item.label}}</label>
            <div class="label-icon" @click="CancelFilterlabel(item, index)"></div>
          </div>
          <div class="toolbar-filter-label">
            <label class="label-filter delete" @click="StartLoad" v-if="BindingFilterValue.length > 0" >Xóa điều kiện lọc</label>
          </div>
        </div>
        <div class="product-content-toolbar-right">
          <MInputSearch @InputWhere="getWhereValue" :placeholder="'Tìm theo mã, tên hàng hóa, dịch vụ'" :style="'width: 250px'" :iconsearch="'icon-search'" />
          <button type="button" class="product-toolbar-load" @click="StartLoad"></button>
          <button type="button" class="product-toolbar-export" @click="getExcel"></button>
          <button type="button" :class="ShowOverviewCss" @click="showOverview"></button>
        </div>
      </div>
      <div class="deletedrop" v-if="isShowDropItemDelete" ref="dropdelete">
        <div class="deletedrop-item">Sửa hàng loạt</div>
        <div class="deletedrop-item" @click="openPopupAsk">Xóa</div>
        <div class="deletedrop-item">Gộp</div>
      </div>
      
      <MTable 
        @custom-open-dbclick="openPopup"
        :RecordsLoad="TableValues"
        @data-load-delete="loadData"
        @get-offset-delete="getOffsetDelete"
        @get-List-Checkbox="getListProduct"
        :closeSelectedAll="closeSelectedAll"
        :SumQuantity="SumQuantity"
        :DeleteCheckbox="ChangeCheckbox"
        :SumExistent="SumExistent"
        :thListTable="thList"
        :tdListTable="tdList"
        :tfoot="true"
        :PopupNotilable="'Vật tư hàng hóa'"
        :baseURL="'baseURLProduct'"
        :TableCheckBox="true"
        @Show-Filter="showFilter"
      />
      
    </div>
    <div class="product-content-bottom">
      <ThePadding
        :TotalCount="TotalCount"
        @filter-padding="getLimitValue"
        @offset-value="getOffSetValue"
        :DeleteMuti="DeleteMutiPadding"
      />
    </div>
    <TheProductSelect v-if="isShowPopupSelect" @close-popup-selete="closePopupSelect" @open-product-popup="openPopup" :ProductID="SaveProductID" />

    <TheProductPopup v-if="isShow" @Edit-Padding="getOffsetDelete" @show-toast="showToastPopup" :BridingCode="SaveSupplyCode" :BridingID="SaveSupplyID" @data-load="loadData" @close-product-popup="closeProductPopup" @custom-handle-click="closeProductPopup" :detailFormMode="Mode" @open-popup-select="openPopupSelect" :property="ProductPopupProperty" :productsSelected="Products" />
    <div ref="filter">
    <MFilter :typeInput="InputType" @get-StatusWarehouse="getStatusWarehouse" :Label="FilterLabel" v-if="isShowFilter" @get-Typesoft="getTypesoft" @start-Filter="loadData" @get-Keyword-Header="getKeywordHeader" @get-Filter-Header="getFilterHeader" :FilterMode="FilterMode" :FilterStyle="StyleFilter" @Close-Filter="closeFilter" />
    </div>
    <MPopupNotification v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteMultiple" :MPopupN = 2 />
    <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>

    <MLoading v-if="LoadingShow" />
  </div>
</template>

<script>
import MButton from '../../components/Base/MButton/MButton.vue';
import MInputSearch from '../../components/Base/MInputSearch/MInputSearch.vue';
import ThePadding from '../../components/Layout/ThePadding/MPadding.vue';
import MToast from '../../components/Base/MToast/MToast.vue';
import MLoading from '../../components/Base/MLoading/MLoading.vue'
import MTable from '../../components/Base/MTable/MTable.vue'
import MPopupNotification from '../../components/Base/MPopupNotification/MPopupNotification.vue';
import TheProductSelect from '../ProductPage/ProductPopup/TheProductSelect.vue';
import TheProductPopup from '../ProductPage/ProductPopup/TheProductPopup.vue';
import MFilter from '../../components/Base/MFilter/MFilter.vue';
import productjs from '../../resouce/product';
import formatjs from '../../resouce/format';
import configs from '../../configs/index';
import enums from '../../resouce/enums';
import toast from '../../resouce/toast';
export default {
    components: {
    MButton,
    ThePadding,
    MLoading,
    MInputSearch,
    MToast,
    MFilter,
    MTable,
    TheProductSelect,
    TheProductPopup,
    MPopupNotification,
  },
  
  methods: {
    /**
    Hàm lấy soft để lọc
    Nguyễn Văn Cương 17/11/2022
    */    
    getFilterHeader(value){
      this.Sort = value;
    },

    /**
    Hàm lấy giá trị để lọc tình trạng tồn kho
    Nguyễn Văn Cương 30/11/2022
     */
    getStatusWarehouse(value){
      this.StatusWarehouse = value;
    }, 

        /**
    Hàm lấy giá trị tìm kiếm để lọc
    Nguyễn Văn Cương 17/11/2022
    */ 
    getKeywordHeader(value){
      if(!this.Keyword.includes(value) && value != null){
        this.Keyword.push(value);
        this.FilterKey = value;
      }
      console.log(this.Keyword); 
    },

   /**
    * hàm click outsite
    * Nguyễn Văn Cương 01/10/2022
   */
   clickEventInterrupt(event){

    if(this.isShowFilter == true){
    //kiểm tra xem con chuột có click vào dropitem
    const isClick = this.$refs.filter.contains(event.target);
     if(!isClick){
      this.isShowFilter = false;
        if(this.FilterMode == 2 && this.FilterKey == null){
          const typesoft = this.TypeSort.indexOf(this.SaveTypeFilter);
          if (typesoft > -1) {
             //loại bỏ phần tử khỏi mảng
            this.TypeSort.splice(typesoft, 1); 
            }
        }
      }
    }
    if(this.isShowDropItemDelete){
      const isClick = this.$refs.dropdelete.contains(event.target);
      if(!isClick){
        this.isShowDropItemDelete = false;
      }
    }
  },  
    /**
     * Hàm mở popup tính chất
     * Nguyễn Văn Cương 01/10/2022
     */
    openPopupSelect(ProductID){
      this.isShowPopupSelect = true;
      if(ProductID){
        this.SaveProductID = ProductID;
      }
    },

    /**
     * Hàm mở overview
     * Nguyễn Văn Cương 01/11/2022
     */
    showOverview(){
      this.isShowOverview = !this.isShowOverview;
      if(this.isShowOverview == true){
         this.ShowOverviewCss = productjs.ShowOverviewCss_1;
         this.TableStyle = productjs.TableStyle_1;
      }else{
        this.ShowOverviewCss = productjs.ShowOverviewCss_2;
        this.TableStyle = productjs.TableStyle_2; 
      }
    },

    /**
     * Hàm mở popup filter
     * Nguyễn Văn Cương 11/11/2022
     */
    showFilter(mode, inputfil, type, filterlabel, posY, posX){

        this.FilterMode = mode;
        //lưu lại loại input của filter
        this.InputType = inputfil;
        //thêm vào mảng
        if(!this.TypeSort.includes(type) && type != null){
          this.TypeSort.push(type);
        }
        this.SaveTypeFilter = type;
        //lưu lại tên trường đang filter
        this.FilterLabel = filterlabel;
        //lấy vị trí hiển thị filter
        posX = posX - 200;
        posY = posY + 20;
        this.isShowFilter = !this.isShowFilter;
        //lưu vị trí hiển thị filter
        this.StyleFilter = "left: " + posX + "px;" + "top: " + posY + "px;";
    },

    /**
     * Hàm lấy tên trường muốn filter
     * Nguyễn Văn Cương 16/11/2022
     */
    getTypesoft(type, filterlabel, inputfilter){
      if(!this.TypeSort.includes(type)){
        this.TypeSort.push(type);
      }
      //lưu lại tên trường đang filter
      this.FilterLabel = filterlabel;
      //lưu lại loại input của filter
      this.InputType = inputfilter;
    },

    /**
     * Hàm đóng filter
     * Nguyễn Văn Cương 18/11/2022
     */
    closeFilter(){
      this.isShowFilter = false;
      this.StartLoad();
    },

    /***
     * Hàm hiển thị những filter đang hoạt động 
     * Nguyễn Văn Cương 18/11/2022
     */
    BindingFilter(){
      if(this.WhereValue){
        this.FilterLabel = null;
      }

      /*
      for (let i = 0; i < this.TypeSort.length; i++) {
        if(this.BindingFilterValue.length > 0){
          if(this.BindingFilterValue[i].value == this.FilterLabel){
            this.BindingFilterValue.splice(i, 1); 
          }
          if(this.BindingFilterValue.length == this.TypeSort.length){
            this.BindingFilterValue.splice(i-1, 1);
          }
        }
      }
      */
        
        if(!this.BindingFilterValue.includes(this.FilterLabel) && this.FilterLabel != null && this.FilterKey != null){
          //trường hợp hiển thị thông thường
          if(this.InputType == 1){
            this.BindingFilterValue.push({label: this.FilterLabel + ": " + this.FilterKey, value: this.FilterLabel});
            
          //phải format tính chất
          }else if(this.InputType == 2){
            this.BindingFilterValue.push({label:this.FilterLabel + ": " + this.formatNatureRecord(this.FilterKey), value: this.FilterLabel});
            
          //phải format thuế
          }else if(this.InputType == 3){
            this.BindingFilterValue.push({label:this.FilterLabel + ": " + this.formatTaxRecord(this.FilterKey), value: this.FilterLabel});
            
          //phải format trạng thái
          }else if(this.InputType == 4){
            this.BindingFilterValue.push({label:this.FilterLabel + ": " + this.formatStatusRecord(this.FilterKey), value: this.FilterLabel});
            
          //phải format trạng thái
          }else{
            this.BindingFilterValue.push({label:this.FilterLabel + ": " + this.formatFilterStatusRecord(this.FilterKey), value: this.FilterLabel});
            
          }
        }
      
    },

    /**
     * hàm format trạng thái
     * Nguyễn Văn Cương 01/10/2022
     */
    formatStatusRecord(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = formatjs.Status_Active;
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = formatjs.Status_UnActive;
       //giá trị 0 là chưa xác định
       }else if (status == enums.UNKNOW){
         return status = formatjs.Status_Unknow;
      //không có cho thành rỗng
       }else{
         return status = "";
       }
    },

    /**
     * hàm format trạng thái
     * Nguyễn Văn Cương 01/10/2022
     */
    formatFilterStatusRecord(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = formatjs.FilterStatus_exist;
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = formatjs.FilterStatus_out_stock;
       //giá trị 0 là chưa xác định
       }else if (status == enums.Out_of_stock){
         return status = formatjs.FilterStatus_not_exist;
      //không có cho thành rỗng
       }else if (status == enums.UNKNOW){
         return status = formatjs.Status_Unknow;
      //không có cho thành rỗng
       }else{
         return status = "";
       }
    },

    /**
     * hàm format thuế
     * Nguyễn Văn Cương 01/11/2022
     */
    formatTaxRecord(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = formatjs.Tax_Active;
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = formatjs.Tax_UnActive;
       //giá trị 0 là chưa xác định
       }else if (status == enums.UNKNOW){
         return status = formatjs.Tax_UnKnow;
      //không có cho thành rỗng
       }else{
         return status = "";
       }
    },

    /**
    hàm format tính chất
    Nguyễn Văn Cương 1/11/2022
     */
    formatNatureRecord(value){
       //giá trị 1 là nữ 
       if(value == enums.Product){
         return value = formatjs.Nature_HH;
      //giá trị 2 là nam
       }else if(value == enums.Service){
         return value = formatjs.Nature_DV;
       //giá trị 0 là khác
       }else if (value == enums.Material){
         return value = formatjs.Nature_NVL;
      //không có cho thành rỗng
       }else if (value == enums.FiProduct){
         return value = formatjs.Nature_TP;
      //không có cho thành rỗng
       }else if (value == enums.Tools){
         return value = formatjs.Nature_CCDC;
      //không có cho thành rỗng
       }else{
         return value = "";
       }
    },

    /**
    hàm xóa phần tử trong combobox nhiều
    Nguyễn Văn Cương 05/10/2022
    */
    CancelFilterlabel(value, indexFilterValue){
      //tìm index của phần từ trong mảng
      const index = this.BindingFilterValue.indexOf(value);
      if (index > -1) {
        //loại bỏ phần tử khỏi mảng
        this.BindingFilterValue.splice(index, 1); 
      }
      //tìm phần từ trang mảng keyword theo value
      for (let i = 0; i < this.Keyword.length; i++) {
        if(indexFilterValue == i){
          const key = this.Keyword.indexOf(this.Keyword[i]);
          if (key > -1) {
            //loại bỏ phần tử khỏi mảng
            this.Keyword.splice(key, 1); 
            this.FilterKey = null;
          }
        }
      }
      //tìm phần từ trang mảng TypeSort theo value
      for (let i = 0; i < this.TypeSort.length; i++) {
        if(indexFilterValue == i){
          const typesoft = this.TypeSort.indexOf(this.TypeSort[i]);
          if (typesoft > -1) {
            //loại bỏ phần tử khỏi mảng
            this.TypeSort.splice(typesoft, 1); 
            this.FilterLabel = null;
            this.loadData();
          }
        }
      }
      
    },

    /**
     * Hàm đóng popup tính chất
     * Nguyễn Văn Cương 01/10/2022
     */
    closePopupSelect(){
      this.isShowPopupSelect = false;
    },

    /**
     * Hàm mở popup hàng hóa
     * Nguyễn Văn Cương 01/10/2022
     */
    openProductPopup(value){
      this.isShow = true;
      this.isShowPopupSelect = false;
      this.ProductPopupProperty = value;
      this.Products = {}
    },

    /**
     * Hàm đóng popup hàng hóa
     * Nguyễn Văn Cương 01/10/2022
     */
    closeProductPopup(){
      this.isShow = false;
    },

    /**
     * Hàm lấy mã  mới
     * Nguyễn Văn Cương 20/11/2022
     */
    async getCodeTable() {
      await fetch(configs.baseURLProduct + productjs.getnewcode, {
        method: "GET", //lấy mã nhân viên cao nhất
      })
        .then((response) => response.json())
        .then((data) => {
          var s = JSON.stringify(data.NewCode);
          this.Products.ProductCode = s.replace(/[^a-zA-Z0-9]*/g, "");
          
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
     * Hàm lấy mã nhóm vật tư hàng hóa mặc định
     * Nguyễn Văn Cương 16/11/2022
     */
    getSupplyCode(property){
        this.SaveSupplyCode = [];
        this.SaveSupplyID = [];
        if(property == 1){
          this.SaveSupplyCode.push(productjs.SupplyCode_HH);
          this.SaveSupplyID.push(productjs.SupplyID_HH);
        }else if(property == 2){
          this.SaveSupplyCode.push(productjs.SupplyCode_DV);
          this.SaveSupplyID.push(productjs.SupplyID_DV);
        }else if(property == 3){
          this.SaveSupplyCode.push(productjs.SupplyCode_NVL);
          this.SaveSupplyID.push(productjs.SupplyID_NVL);
        }else if(property == 4){
          this.SaveSupplyCode.push(productjs.SupplyCode_TP);
          this.SaveSupplyID.push(productjs.SupplyID_TP);
        }else{
          this.SaveSupplyCode.push(productjs.SupplyCode_CDCC);
          this.SaveSupplyID.push(productjs.SupplyID_CDCC);
        }
    },

    /**
     * Hàm mở popup thêm nhân viên
     * và lấy dữ liệu nhân viên theo id dể hiện trên popup
     * Nguyễn Văn Cương 25/09/2022
     */
    async openPopup(id, detailFormMode, value) {
      //trường hợp lấy dữ liệu nhân viên theo id dể hiện trên popup
      if (id) {
        this.LoadingShow = true; //hiển thị loading
        await fetch(configs.baseURLProduct + id, { method: "GET" })
          .then((res) => res.json())
          .then(async (data) => {
            this.LoadingShow = false; //Đóng loading
            this.Products = data;
            //nhân bản
            if(detailFormMode == 1){
              await this.getCodeTable();
              await this.getSupplyCode(value);
            }
            this.Mode = detailFormMode;
            if (this.Products.SupplyCode) {
              this.Products.SupplyCode =
              this.Products.SupplyCode.split(",");
            }
            this.SaveSupplyCode = this.Products.SupplyCode;
            console.log(this.Mode);
            this.isShow = true; //Hiển thị popup
            this.isShowPopupSelect = false;
            this.isShowToast = false;
            if(value){
              this.ProductPopupProperty = value;
            }else{
              this.ProductPopupProperty = this.Products.Nature;
            }
            
          })
          .catch((res) => {
            console.log(res);
          });
        //trường hợp chỉ mở popup
      } else {
        (this.Products = {}), //dữ liệu trên popup rỗng
          await this.getCodeTable();
          await this.getSupplyCode(value);
        this.Mode = detailFormMode;
        this.isShowPopupSelect = false;
        this.ProductPopupProperty = value;
        this.isShow = true;
      }
    },

    /**
    Hàm mở dropbox xóa
    Nguyễn Văn Cương 23/11/2022
     */
    openDropDelete(){
      if(this.listProDelete.length > 1){
        this.isShowDropItemDelete = !this.isShowDropItemDelete;
      }
      if(this.listProDelete.length == 0 || this.listProDelete == []){
        this.isShowDropItemDelete = false;
      }
    },
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(){  
      this.isShowAskDelete = true;
    },
    /**
     * hàm đóng popup thêm nhân viên
     * Nguyễn Văn Cương 25/09/2022
     */
    closePopup() {
      this.isShow = false;
      if (this.Products.ProductID == null) {
        this.Products = {};
      }
      this.isShowToast = false;
    },
    
      /**
     * Hàm đóng popup hỏi người dùng có xóa không
     * Nguyễn Văn Cương 25/09/2022
     */
    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.isShowToast = false;
    },

    /**
     * lấy số lượng bản ghi hiển thị
     * Nguyễn Văn Cương 25/09/2022
     */
    getLimitValue(limits) {
      if(this.StatusWarehouse == 0){
        this.Offsetwarehousestatus = limits;
        this.Limitwarehousestatus = this.Offsetwarehousestatus * 2;
        this.BridingTable();
      }else{
        this.LimitValue = limits;
        this.loadData();
      }

    },

    /**
     * lấy trang hiển thị sau khi xóa hết bản ghi trong trang
     * Nguyễn Văn Cương 1/12/2022
     */
    getOffsetDelete(){
      this.OffSetValue = 0;
      this.DeleteMutiPadding = true;
      this.loadData();
    },

    /**
     * lấy trang hiển thị
     * Nguyễn Văn Cương 25/09/2022
     */
    getOffSetValue(offset) {
      if(this.StatusWarehouse == 0){
        this.Offsetwarehousestatus = offset;
        this.Limitwarehousestatus = offset + this.Limitwarehousestatus;
        this.BridingTable();
      }else{
        this.OffSetValue = offset;
        this.loadData();
      }

    },

    /**
     * lấy dữ liệu cần tìm kiếm
     * Nguyễn Văn Cương 25/09/2022
     */
    getWhereValue(where) {
      /*
      if(this.timeout){
        clearTimeout(this.timeout)
        this.timeout = null;
      }
      else{
        this.timeout = setTimeout(() => {
        this.WhereValue = where;
        this.loadData();
        }, 1000);
      }
      */
     this.WhereValue = where;

      if(where == ""){
        this.WhereValue = null;
        this.loadData();
      }
    },
    /**
     * Hàm lấy danh sách mã nhân viên cần xóa
     * Nguyễn Văn Cương 15/10/2022
     */
    getListProduct(listProDe, status) {
      this.listProDelete = listProDe;
      this.CheckAll = status;
    },
    
    /**
    Hàm hiện thị thông báo
    Nguyễn Văn Cương 15/10/2022
     */
    ShowToast(Tstatus){
        this.isShowToast = true; 
        if(Tstatus == true){
          this.Toastcssicon = toast.Toastcssicon_sus;
          this.Toastcss = toast.Toastcss_sus;
          this.ToastMess_color = toast.ToastMess_color_sus;
          this.ToastMess = toast.ToastMessDeleteMuti_sus;
        }else{
          this.Toastcssicon = toast.Toastcssicon_faild;
          this.Toastcss = toast.Toastcss_faild;
          this.ToastMess_color = toast.ToastMess_color_faild;
          this.ToastMess = toast.ToastMessDeleteMuti_faild;
        }
        this.timeout = null;
        this.closeToast();
    },
    /**
    Hàm hiện thị thông báo cho popup 
    Nguyễn Văn Cương 15/10/2022
     */
    showToastPopup(Toastcssicon, Toastcss, ToastMess_color, ToastMess){
        this.isShowToast = true; 
        this.Toastcssicon = Toastcssicon;
        this.Toastcss = Toastcss;
        this.ToastMess_color = ToastMess_color;
        this.ToastMess = ToastMess;
        this.timeout = null;
        this.closeToast();
    },
    
    /***
     * Hàm tự động đóng toast
     * Nguyễn Văn Cương 1/11/2022
     */
    closeToast(){
      if(this.timeout){
        clearTimeout(this.timeout)
        this.timeout = null;
      }
      else{
        this.timeout = setTimeout(() => {
        this.isShowToast = false;
        }, 4000);
      }
    },

    /**
     * Hàm thực hiện xóa nhiều nhân viên
     * Nguyễn Văn Cương 15/10/2022
     */
    async deleteMultiple() {
      var listD = this.listProDelete;
      
      await fetch(configs.baseURLProduct + productjs.batchdelete, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(listD),
      })
        .then((res) => res.json())
        .then((data) => {
          if (data.errorCode) {
              //mở popup thông báo
              this.isShowNotification = true;
              if (data.errorCode) {
                this.errors = data.moreInfo;
              }
            }else{
              //load lại data
              this.ClosePopupAsk();
              if(this.CheckAll){
                this.OffSetValue = 0;
                this.DeleteMutiPadding = true;
              }
              this.ChangeCheckbox = true;
              this.loadData();
              this.closeSelectedAll = true;
              this.ShowToast(this.ToastStatus = true);
              console.log(data);
            }
          
        })
        .catch((res) => {
          this.ShowToast(this.ToastStatus = false);
          console.log(res);
        });
    },
    /**
    Hàm chuẩn bị load lại dữ liệu
    Nguyễn Văn Cương 17/11/2022
     */
    StartLoad(){
      this.LimitValue = null;
      this.OffSetValue = null;
      this.WhereValue = null;
      this.Sort = 0;
      this.TypeSort = [];
      this.Keyword = [];
      this.FilterKey = null;
      this.FilterLabel = null;
      this.BindingFilterValue = [];
      this.StatusWarehouse = null;
      this.DeleteMutiPadding = false;
      this.loadData();
    },

    /**
     * Hàm xóa giá trị tình trạng tồn kho
     * Nguyễn Văn Cương 29/11/2022
     */
    ResetStatusWarehouse(){
      this.FilterStatus_out_stock = 0;
      this.ProductStatus_out_stock = [];
      this.FilterStatus_exist = 0;
      this.ProductStatus_exist = [];
      this.FilterStatus_not_exist = 0;
      this.ProductStatus_not_exist = []; 
    },

    /**
     * Hàm hiển thị dữ liệu lên bảng
     * Nguyễn Văn Cương 29/11/2022
     */
    BridingTable(){
      if(this.StatusWarehouse == 0){
        switch (this.FilterKey) {
          //trường hợp tất cả
          case 0:
            this.TableValues = this.ProductsTable;
            this.TotalCount = this.Totalcount;
            break;
          //trường hợp còn tồn
          case 1:
            this.TableValues = this.ProductStatus_exist.slice(this.Offsetwarehousestatus,this.Limitwarehousestatus);
            this.TotalCount = this.FilterStatus_exist
            break;
          //trường hợp sắp hết hàng
          case 2: 
            this.TableValues = this.ProductStatus_out_stock.slice(this.Offsetwarehousestatus,this.Limitwarehousestatus);
            this.TotalCount = this.FilterStatus_out_stock;
            break;
          //trường hợp hết hàng
          case 3: 
            this.TableValues = this.ProductStatus_not_exist.slice(this.Offsetwarehousestatus,this.Limitwarehousestatus);
            this.TotalCount = this.FilterStatus_not_exist;
            break;
          default:
            //trường hợp tất cả
            this.TableValues = this.ProductsTable;
            this.TotalCount = this.Totalcount;
            break;
        }
      }else{
        this.TableValues = this.ProductsTable;
        this.TotalCount = this.Totalcount;
      }
    },

    /**
     * hàm load dữ liệu
     * Nguyễn Văn Cương 15/09/2022
     */
    loadData() {
      console.log(1);
      this.LoadingShow = true;
      this.ResetStatusWarehouse();
      var limit = this.LimitValue; //lưu số lượng bản ghi
      if (limit == null) {
        //nếu không có, mặc định là 10
        limit = 10;
      }
      var offset = this.OffSetValue; //lưu trang hiển thị
      
      if (offset == null) {
        //nếu không có, mặc định là 0
        offset = 0;
      }
      
      var where = this.WhereValue; //lưu dữ liệu tìm kiếm
      if (where == null) {
        //nếu không có, mặc định là rỗng
        where = "";
      }
      else{
        offset = 0;
      }
      var soft = this.Sort;
      var typesort = [];
      for (let i = 0; i < this.TypeSort.length; i++) {
         typesort += "&typesoft=" + this.TypeSort[i];
         offset = 0;
      }
      var keyword = [];
      for (let i = 0; i < this.Keyword.length; i++) {
        keyword += "&keyword=" + this.Keyword[i];
      }
      const filter = `filter?wnere=${where}&limit=${limit}&offset=${offset}&soft=${soft}${typesort}${keyword}`;
      fetch(configs.baseURLProduct + filter, {
        method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.ProductsTable = data.data; //lưu dữ liệu
          this.Totalcount = data.totalCount;
          if(!this.DeleteMutiPadding){
            this.CaculateWarehouseStatus();
          }
          this.BridingTable();
          if(this.StatusWarehouse == 0){
            this.SumQuantity = null;
            this.SumExistent = null;
          }else{
            this.SumQuantity = data.sumQuantity;
            this.SumExistent = data.sumExistent;
          }
          this.BindingFilter();
          this.LoadingShow = false; //tắt loading
        })
        .catch((res) => {
          console.log(res);
        });
    },
   
    /**
    Hàm tạo file excel danh sách nhân viên
    Nguyễn Văn Cương 15/10/2022
     */
    getExcel(){
      try{
        //hiển loading
        this.LoadingShow = true;
       //Gọi API
        fetch(configs.baseURLProduct + productjs.getproductexcel,{method: "GET"})
        .then((t)=>{
            return t.blob().then((b)=>{
              //tạo thẻ a
              var a = document.createElement("a");
              //lấy ra URL
              a.href = URL.createObjectURL(b);
              // Set attribute của thẻ a và tên của file excel
              a.setAttribute("download", "Danh_sach_hang_hoa_dich_vu.xlsx");
              a.click();
              // Ẩn Loading
              this.LoadingShow = false;
            }).catch((res) => {
              console.log(res);
            });
        })
      }catch(error){
          console.log(error)
      }
    },
    /**
     * Hàm phím tắt Enter mở popup
     * Nguyễn Văn Cương 15/09/2022
     */
    handleEventInterrupt(event){
        if(event.keyCode == enums.ENTER){
           this.loadData();
        }
    },
    
    /**
     * hàm đóng popup thông báo
     * Nguyễn Văn Cương 15/09/2022
     */
    closeNoti() {
      this.isShowNotification = false;
      this.validate = false;
    },

    /**
     * Hàm lấy mã  mới
     * Nguyễn Văn Cương 20/11/2022
     */
    getAll() {
      fetch(configs.baseURLProduct, {
        method: "GET", //lấy mã nhân viên cao nhất
      })
        .then((response) => response.json())
        .then((data) => {
          this.AllProduct = data;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    /**
     * Hàm tính toán tình trạng tồn kho
     * Nguyễn Văn Cương 28/11/2022
     */
     CaculateWarehouseStatus(){
        for (let i = 0; i < this.AllProduct.length; i++) {
            //trường hợp sắp hết hàng khi số lượng tồn tối thiểu lớn hơn số lượng tồn
            if(this.AllProduct[i].amount > this.AllProduct[i].quantityStock && this.AllProduct[i].quantityStock != null){
              if(!this.ProductStatus_out_stock.includes(this.AllProduct[i])){
                this.FilterStatus_out_stock = this.FilterStatus_out_stock + 1;
                this.ProductStatus_out_stock.push(this.AllProduct[i]);
              }
            //trường hợp hết hàng khi số lượng tồn bằng 0
            }else if(this.AllProduct[i].quantityStock == null){
              if(!this.ProductStatus_not_exist.includes(this.AllProduct[i])){
                this.FilterStatus_not_exist = this.FilterStatus_not_exist  + 1;
                this.ProductStatus_not_exist.push(this.AllProduct[i]);
              }
            //trường hợp còn tồn khi số lượng tồn tối thiểu nhỏ hơn số lượng tồn
            }else{
              if(!this.ProductStatus_exist.includes(this.AllProduct[i])){
                this.FilterStatus_exist = this.FilterStatus_exist  + 1;
                this.ProductStatus_exist.push(this.AllProduct[i]);
              }
            }
       }
     },
  },
  created() {
    this.getAll();
    this.loadData();
  },
  updated(){
    this.getAll();
  },
  mounted(){
    //gọi hàm phím tắt
      window.addEventListener('keyup', this.handleEventInterrupt);
      //gọi hàm clickoutsite
      window.addEventListener('mousedown', this.clickEventInterrupt);
      
  },
  unmounted(){
    //xóa hàm phím tắt
      window.removeEventListener('keyup', this.handleEventInterrupt);
      //xóa hàm clickoutsite
      window.removeEventListener('mouseup', this.clickEventInterrupt);
  },

   data() {
    return {
       //gọi popup lựa chọn tính chất
      isShowPopupSelect: false,
      //lưu tính chất
      ProductPopupProperty: 1,
      //gọi popup thêm nhân viên
      isShow: false, 
       //gọi màn hình loadind
      LoadingShow: false,
       //lưu giá trị nhân viên
      Products: null,
      //lưu giá trị bảng hàng hóa
      ProductsTable: null, 
      //lưu giá trị để hiển thị lên table
      TableValues: null,
      //lưu giá trị số lượng trang
      LimitValue: null, 
      //lưu giá trị bản ghi hiện tại
      OffSetValue: null, 
      //lưu giá trị tìm kiếm
      WhereValue: null, 
      //lưu danh sách mã nhân viên cần xóa
      listProDelete: [], 
      //lưu trạng thái mở popup nhân viên 
      Mode: 2, 
      //gọi popup hỏi có xóa không
      isShowAskDelete: false, 
      //đóng chọn checkbox
      closeSelectedAll: false, 
      //hiển thị thông báo
      isShowToast: false, 
       //trang thái thông báo
      ToastStatus: true,
      //nội dung thông báo
      ToastMess:{}, 
       //màu nội dung thông báo
      ToastMess_color: {},
      //css thông báo
      Toastcss:{}, 
      //icon thông báo
      Toastcssicon: {}, 
      //mảng chưa keyCode
      arrKeyCode: [],
      //lưu thời gian delay khi tìm kiếm
      timeout: null,
      //lưu cảnh báo thiếu dữ liệu
      errors: [],
      //gọi popup thiếu dữ liệu
      isShowNotification: false,
      //trạng thái nút
      ButtonMode: 1,
      //lưu tổng trang mặc định
      TotalCount: 10,
      //lưu tổng số Số lượng tồn mặc định
      SumQuantity: 0,
      //lưu tổng số Giá trị tồn tối thiểu
      SumExistent: 0,
      //hiển thị overview
      isShowOverview: true,
      //lưu css overview
      ShowOverviewCss: "product-toolbar-hideoverview",
      //lưu style của table
      TableStyle: {},
      //trạng thái mở filter
      isShowFilter: false,
      //lưu css filter
      StyleFilter: {},
      //lưu lại id của product
      SaveProductID: null,
      //lưu giá trị lọc
      Sort: null,
      //lưu tên trường cần soft
      TypeSort: [],
      //lưu giá trị tìm kiếm nhập vào theo cột
      Keyword: [],
      //lưu giá trị input của filter
      InputType: null,
      //lưu giá trị biding hiển thị
      BindingFilterValue: [],
      //lưu tên head muốn filter
      FilterLabel: null,
      //lưu keyword filter
      FilterKey: null,
      //lưu lại code nhóm vật tư hàng hóa
      SaveSupplyCode: [],
      //lưu lại id nhóm vật tư hàng hóa
      SaveSupplyID: [],
      //trạng thái hiển thị dropbox xóa
      isShowDropItemDelete: false,
      //lưu trạng thái xóa nhiều
      DeleteMutiPadding: false,
      //lưu tên trường filter
      SaveTypeFilter: null,
      //lưu số lượng còn tồn
      FilterStatus_exist: 0,
      //lưu mảng số lượng tồn kho
      ProductStatus_exist: [],
      //lưu số lượng sắp hết hàng
      FilterStatus_out_stock: 0,
      //lưu mảng số lượng sắp hết hàng
      ProductStatus_out_stock: [],
      //lưu số lượng hết hàng
      FilterStatus_not_exist: 0,
      //lưu mảng số lượng hết hàng
      ProductStatus_not_exist: [],
      //lưu trạng thái tình trạng tồn kho
      StatusWarehouse: null,
      //lưu trạng thái xóa check box
      ChangeCheckbox: false,
      //lưu trạng thái chọn nhiều để xóa hay không
      CheckAll: false,
      //lưu tổng số lượng
      Totalcount: null,
      //lưu toàn bộ bản ghi danh sách hàng hóa
      AllProduct: null,
      //lưu bản ghi bắt đầu để lọc tình trạng tồn kho
      Offsetwarehousestatus: 0,
      //lưu số lượng bản ghi để lọc tình trạng tồn kho
      Limitwarehousestatus: 10,
      //lưu giá trị tdhead của table
      thList: [
        {style: "min-width: 150px;", label: "TÊN", filterlabel: "Tên", property: "ProductName", inputfilter: 1},
        {style: "min-width: 100px;", label: "MÃ", filterlabel: "Mã", property: "ProductCode", inputfilter: 1},
        {style: "min-width: 100px;", label: "GIẢM THUẾ THEO NQ 43", filterlabel: "Giảm thuế theo NQ 43", class: "tab", span: "Trạng thái tra cứu giảm thuế theo Nghị quyết 43/2022/QH15", property: "TaxReduction", inputfilter: 3 },
        {style: "min-width: 100px;", label: "TÍNH CHẤT",filterlabel: "Tính chất", property: "Nature", inputfilter: 2},
        {style: "min-width: 100px;", label: "NHÓM VTHH", filterlabel: "Nhóm VTHH", class: "tab-b", span: "Nhóm vật tư hàng hóa", property: "s.SupplyCode", inputfilter: 1},
        {style: "min-width: 100px;", label: "ĐƠN VỊ TÍNH", filterlabel: "Đơn vị tính", property: "u.UnitCalculateValue", inputfilter: 1},
        {style: "min-width: 115px;", label: "SỐ LƯỢNG TỒN", filterlabel: "Số lượng tồn", class: "tab-th-amount", property: "QuantityStock" , inputfilter: 1},
        {style: "min-width: 100px;", label: "GIÁ TRỊ TỒN", filterlabel: "Giá trị tồn", class: "tab-th-amount",property: "ExistentialValue", inputfilter: 1},
        {style: "min-width: 100px;", label: "THỜI GIAN BẢO HÀNH", filterlabel: "Thời gian bảo hành", property: "Insurance", inputfilter: 1},
      ],
      //lưu giá trị property td
      tdList: 
      [{property: "productName"},
      {property: "productCode"},
      {property: "taxReduction", fun: 4}, 
      {property: "nature", fun: 5},
      {property: "supplyCode"},
      {property: "unitCalculateValue"},
      {property: "quantityStock", fun: 6, class: "product-tab-th-amount"},
      {property: "existentialValue", fun: 6, class: "product-tab-th-amount"},
      {property: "insurance"},
      {property: "productID", style: "display: none"}
      ],
    };
  },
}
</script>

<style>
:root {
  --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}.Productcontainer{
    width: 100%;
    height: 100%;
}
.product-content-top {
  width: 100%;
  height: 60px;
  display: flex;
}
.product-popup {
  z-index: 4;
}
.product-content-top-left {
  width: 35%;
  height: 70px;
  float: left;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.product-content-top-left-label {
  width: 350px;
  height: 70px;
  background-color: rgb(236, 238, 241);
  font-family: Misa Fonts Bold;
}
.product-content-top-label {
  height: 60px;
  width: 300px;
  font-size: 24px;
  font-weight: 700;
}
.product-content-top-right {
  width: 70%;
  height: 70px;
  float: right;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.product-content-toolbar {
  display: flex;
  position: sticky;
  height: 50px;
  background-color: #ffff;
}.product-content-toolbar-left-icon{
  background-image: var(--icon);
  background-position: -259px -147px;
  background-repeat: no-repeat;
  width: 20px;
  height: 20px;
  margin-top: 15px;
}.product-content-toolbar-left{
  width: 100%;
  display: flex;
}
.product-content-toolbar-right {
  position: absolute;
  right: 0;
  display: flex;
  margin-top: 5px;
}
.product-toolbar-input {
  height: 30px;
  width: 200px;
  border-width: 1px;
  border-radius: 4px;
  border-color: #bbbbbb;
  color: #000;
  padding: 0 16px;
  border-style: solid;
  outline: unset;
}
.product-toolbar-input:focus {
  border-color: #019160;
}
.product-toolbar-load {
  width: 30px;
  height: 30px;
  background-image: var(--icon);
  background-repeat: no-repeat;
  background-position: -425px -198px;
  border: none;
  border-radius: 4px;
  background-color: #ffff;
  cursor: pointer;
  margin-left: 10px;
}.product-toolbar-export{
   width: 40px;
  height: 30px;
  background-image: var(--icon);
  background-repeat: no-repeat;
  background-position: -611px -834px;
  border: none;
  border-radius: 4px;
  background-color: #ffff;
  cursor: pointer;
}.product-guid{
  position: absolute;
  top: 10px;
  right: 350px;
  display: flex;
  width: 200px;
  height: 40px;
}.product-guid-icon{
  background-image: var(--icon);
  background-position: -984px -145px;
  background-repeat: no-repeat;
  width: 24px;
  height: 24px;
}.product-guid a{
  margin-left: 7px;
  color: #009AD5;
  text-decoration: none;
}.product-guid a:hover{
  text-decoration: underline;
}.product-content-top-underlabel{
  margin-top: 5px;
  color: #009AD5;
  display: flex;
  font-family: Misa Fonts Regular;
}.product-content-top-underlabel-icon{
  margin-top: 3px;
  background: var(--icon) no-repeat -228px -362px;
	width: 12px;
	height: 12px;
}.product-overview{
   width: 100%;
   height: 110px;
   background-color: #ffff;
   border-bottom: 24px solid rgb(236, 238, 241);
   display: flex;
   margin-top: 10px;
   position: relative;
}
.product-overview-ri{
  width: 50%;
  height: 100%;
  position: relative;
}.product-overview-ri-left{
  width: 150px;
  height: 80%;
  position: absolute;
  right: 0;
  padding-right: 40px;
  text-align: right;
  margin-top: 15px;
  border-right: 1px solid #000;
}.product-overview-ri-icon{
  margin-right: 15px;
  position: absolute;
  background-image: var(--icon);
  background-position: -27px -1311px;
  background-repeat: no-repeat;
  width: 90px;
  height: 90px;
  right: 150px;
  margin-top: 10px;
  border-radius: 50%;
  border: 2px solid #ffff;
}.product-overview-ri-icon:hover{
   border: 2px solid rgba(254,167,17,.3);
}.product-overview-ri-icon:active{
  border: 1px solid rgba(254,167,17,.3);
}
.product-overview-ri-left-top{
  color: #fea711;
  height: 30px;
  font-size: 38px;
  line-height: 38px;
}.product-overview-ri-left-center,.overview-le-left-center{
  margin-top: 10px;
  color: #000;
  font-size: 14px;
  font-weight: 700;
}.product-overview-ri-left-bottom,.overview-le-left-bottom{
  height: 25px;
  color: #8c8c8e;
  font-size: 16px;
  text-transform: uppercase;
}
.product-overview-le-icon{
  position: absolute;
  background-image: var(--icon);
  background-position: -139px -1307px;
  background-repeat: no-repeat;
  width: 100px;
  height: 100px;
  margin-left: 50px;
  width: 90px;
  height: 90px;
  margin-top: 10px;
  border-radius: 50%;
  border: 2px solid #ffff;
}.product-overview-le-icon:hover{
  border: 2px solid rgba(255,0,0,.3);
}.product-overview-le-left{
  width: 150px;
  height: 80%;
  margin-left: 150px;
  padding-right: 40px;
  text-align: left;
  margin-top: 15px;
  margin-left: 170px;
}.product-overview-le-left-top{
  color: #eb1d1d;
  height: 30px;
  font-size: 38px;
  line-height: 38px;
}.product-overview-reload{
  position: absolute;
  background-image: var(--icon);
  background-position: -1098px -90px;
  background-repeat: no-repeat;
  right: 0;
  top: 0;
  width: 25px;
  height: 25px;
  margin-top: 3px;
}.product-tooltip{
    width: 100px;
    height: 18px;
    font-size: 12px;
    position: absolute;
    top: 50px;
    right: 50px;
    background-color: #505050;
    border-radius: 4px;
    padding: 2px 4px;
    z-index: 5;
    text-align: center;
    color: #fff;
    visibility: hidden;
}.product-overview-ri-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
}.product-overview-le-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
}.product-toolbar-hideoverview{
  position: absolute;
  background-image: var(--icon);
  background-position: -124px -356px;
  background-repeat: no-repeat;
  right: -28px;
  top: -25px;
  width: 25px;
  height: 25px;
  border: 1px solid #e2e9f2;
  background-color: #f2f5f8;
}.product-toolbar-hideoverview.b{
  background-position: -172px -356px;
}.content-table{
  height: calc(100% - 280px);
  width: calc(100% - 30px);
  background-color: #fff;
  padding: 10px 16px;
  border-radius: 4px 4px 0px 0px;
}.product-content-toolbar-left-btn{
  width: 145px;
  height: 100%;
}.toolbar-filter-label{
  min-width: 40px;
  margin-right: 5px;
  height: 32px;
  display: flex;
  align-items: center;
}.label-filter{
  color: #009AD5;
}.label-icon{
  background-image: var(--icon);
  background-position: -77px -311px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
}.label-filter.delete:hover{
  text-decoration: underline;
}.deletedrop{
    width: 100px;
    height: 80px;
    position: absolute;
    z-index: 10;
    background-color: #fff;
    border: 1px solid #bbbbbb;
    border-radius: 4px;
    text-align: left;
    top: 275px;
    left: 129px;
    font-size: 13px;
    
}.deletedrop-item{
  width: 93%;
  height: 25px;
  padding-top: 2px;
  padding-left: 7px;
}.deletedrop-item:hover{
  background-color: #E8E9EC;
  
}
</style>