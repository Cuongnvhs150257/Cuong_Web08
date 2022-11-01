<template>
  <div class="product-contentner"> 
    <table>
    <thead>
      <tr>
        <th class="product-box"><MCheckbox @click="handleCheckBoxAll" :stateCheckAll="stateCheckAll"  /></th>
        <th style="min-width: 250px;">TÊN</th>
        <th style="min-width: 120px;">MÃ</th>
        <th style="min-width: 120px;">GIẢM THUẾ THEO NQ 43</th>
        <th style="min-width: 120px;">TÍNH CHẤT</th>
        <th style="min-width: 120px;">Nhóm VTHH</th>
        <th style="min-width: 120px;" class="product-tab-th-select-amount">SỐ LƯỢNG TỒN</th>
        <th class="product-tab-th-select-amount" style="min-width: 140px;" >GIÁ TRỊ TỒN</th>
        <th class="product-tab-th-select">CHỨC NĂNG</th>
      </tr>
    </thead>
    <tbody v-if="ProductsLoad">
      <tr
        v-for="pro in ProductsLoad.data"
        :key="pro.productID"
        @dblclick="rowDBClick(pro.productID)"
      >
        <td class="product-box"><MCheckbox @click="handleCheckBox(pro.productID)"  :stateCheckAll="stateCheckAll" /></td>
        <td>{{ pro.productName }}</td>
        <td>{{ pro.productCode }}</td>
        <td>{{ this.fomatTaxReduction(pro.taxReduction)}}</td>
        <td>{{ pro.nature }}</td>
        <td>{{ pro.supplyName}}</td>
        <td  class="product-tab-th-select-amount" >{{ pro.quantityStock }}</td>
        <td  class="product-tab-th-select-amount" >{{ pro.existentialValue }}</td>
        <td style="min-width: 110px;">
          <label class="product-tab-th-select-lable" @click="rowDBClick(pro.productID)">Sửa</label>
          <div class="product-btnopendrop"></div>
        </td>
      </tr>
    </tbody>
  </table>
  <div class="product-mpopup-ask">
    <MPopupAsk v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteProduct" :getProductCode="getproductdeteteCode"/>
  </div>
  <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>
  </div>
  
</template>

<script>
import MPopupAsk from '../../components/Base/MPopupAsk/MPopupAsk.vue';
import MCheckbox from "../../components/Base/MCheckbox/MCheckbox.vue";
import MToast from '../../components/Base/MToast/MToast.vue';
import configs from '../../configs/index';
import enums from '../../resouce/enums';
import toast from '../../resouce/toast';

export default {
  name: "ProductList",
  props: {
    ProductsLoad: Object,
    closeSelectedAll: Boolean
  },
  
  methods: {

    /**
     * Hàm thực hiện kích hoạt toàn bộ checkbox
     * Nguyễn Văn Cương 15/10/2022
     */
    handleCheckBoxAll(){
      //chuyển trạng thái checkbox
      this.stateCheckAll = !this.stateCheckAll;
      if(this.stateCheckAll == true){
          //vòng lặp thêm mã nhân viên vào mảng
          this.ProductsLoad.data.forEach(pro => {
            this.listProSelected.push(pro.productID);
          },
          this.$emit("get-List-Product", this.listProSelected)
          );
      }else{
        this.listProSelected = [];
      }
    },

    /**
     * hàm thực hiện kích hoạt checkbox
     */
    handleCheckBox(ProID){
          //thêm mã nhân viên đã chọn vào mảng
          this.listProSelected.push(ProID);
          this.$emit("get-List-Product", this.listProSelected)
          console.log(this.listProSelected);
    },

    /**
     * hàm hiện thông tin trên popup khi nhấn vào Sửa
     * Nguyễn Văn Cương 25/09/2022
     */
    rowDBClick(productID) {
      //bấm dbclick để sửa
      this.detailFormMode = 2;
      this.$emit("custom-open-dbclick", productID, this.detailFormMode);
    },

    /**
     * hàm lấy thông tin nhân viên khi xóa
     * Nguyễn Văn Cương 25/09/2022
     */
    getProductDetele(propductID, productCode){
        this.getproductdetetevalue = propductID;
        this.getproductdeteteCode = productCode;
        
    },
 
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(selectedit){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        console.log(this.checkDelete);
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.idProductDelete = this.getproductdetetevalue; //lưu id employee cần xóa
        }else if(this.checkDelete == 1){
          //bấm nhân bản
           this.detailFormMode = 1;
           this.$emit("custom-open-dbclick", this.getproductdetetevalue, this.detailFormMode);
        }else{
           console.log(1);
           //ngưng sử dụng
        }
    },

    /**
     * Hàm đóng popup hỏi người dùng có xóa không
     * Nguyễn Văn Cương 25/09/2022
     */
    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.popupAskCance = false; //lưu trạng thái đóng popup hỏi người dùng
    },

    /**
     * hàm format giới tính 
     * Nguyễn Văn Cương 01/10/2022
     */
    fomatTaxReduction(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = "Có giảm thuế";
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = "Không giảm thuế";
       //giá trị 0 là chưa xác định
       }else if (status == enums.UNKNOW){
         return status = "Chưa xác định";
      //không có cho thành rỗng
       }else{
         return status = "";
       }
    },

    /**
     * hàm format ngày tháng 
     * Nguyễn Văn Cương 01/10/2022
     */
    formatDate(date) {
    try {
      
      if (date) {
        var format = "nn/mm/YYYY";
        date = new Date(date);
        // Lấy ra ngày
        let day = date.getDate();
        day = day < 10 ? `0${day}` : day;
        // Lấy ra tháng
        let month = date.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        // Lấy ra năm
        let year = date.getFullYear();
        if(format != "dd/MM/YYYY") return `${day}/${month}/${year}`
        return `${year}-${month}-${day}`;
      } else {
        return "";
      }
    } catch (error) {
      console.log(error);
    }
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
          this.ToastMess = toast.ToastMessDelete_sus;
        }else{
          this.Toastcssicon = toast.Toastcssicon_faild;
          this.Toastcss = toast.Toastcss_faild;
          this.ToastMess_color = toast.ToastMess_color_faild;
          this.ToastMess = toast.ToastMessDelete_faild;
        }
    },

    /**
     * Hàm xóa employee theo id 
     * Nguyễn Văn Cương 25/09/2022
     */
    async deleteProduct() {
          this.isShowToast = false;
          //lấy employeeid đã lưu 
          var id = this.idProductDelete; 
           //check xem người dùng có ấn hủy hay không
          if(this.popupAskCance == true){
          {
            //đóng popup hỏi người dùng
            this.ClosePopupAsk();
              await fetch(
            configs.baseURLProduct + id,
            { method: "DELETE" }
          )
            .then((res) => res.json())
            .then((data) => {
              //load lại data
              this.$emit("data-load-delete");
              this.popupAskCance = true;
              this.ShowToast(this.ToastStatus = true);
              console.log(data);
             
            })
            .catch((res) => {
              this.ShowToast(this.ToastStatus = false);
              console.log(res);
            });
          }
          }
    },
    
  },
  created() {
  },
  data() {
    return {
      products: [], //lưu dữ liệu nhân viên
      proSelected: {}, //lưu nhân viên đã chọn
      detailFormMode: 1, //lưu trạng thái mở popup
      isShowAskDelete: false, //gọi popup hỏi có xóa không
      popupAskCance: true, //nút hủy xóa
      idProductDelete:0, //lưu id nhân viên cần xóa
      checkDelete: 2,  //trạng thái xóa
      getproductdetetevalue: 0, //lưu id nhân viên cần xóa
      getproductdetetecode: '', //lưu code nhân viên cần xóa
      stateCheckAll: false, //lưu trạng thái checkbox
      listEmpSelected: [], //lưu danh sách nhân viên cần xóa
      isShowToast: false, //hiển thị thông báo
      ToastStatus: true, //trang thái thông báo
      ToastMess:{}, //nội dung thông báo
      ToastMess_color: {}, //màu nội dung thông báo
      Toastcss:{}, //css thông báo
      Toastcssicon: {}, //icon thông báo
     
    };
  },
  components: {
    MPopupAsk,
    MCheckbox,
    MToast,
  }
};
</script>

<style>

.product-content-table {
  height: calc(100% - 276px);
  width: calc(100% - 30px);
  background-color: #fff;
  padding: 10px 16px;
  border-radius: 4px 4px 0px 0px;
}
.product-contentner{
  height: calc(100% - 40px);
  width: 100%;
  overflow: scroll;
  border-radius: 4px;
}
::-webkit-scrollbar {
  height: 8px;
  width: 8px;
}
::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;

}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #bbb;
  border-radius: 4px;
  cursor: pointer;
  width: 8px;
  height: 8px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #7c869c;
}
.product-content-table table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: unset;
  font-size: 13px;

}

td, th {
  border-bottom: 1px solid rgb(216, 211, 211);
  border-right: 1px solid rgb(216, 211, 211);
  height: 48px;
  font-size: 13px;

}
.product-content-table tr td,
.product-content-table tr th {
  padding-left: 8px;
  text-align: left;
  cursor: pointer;
}.product-content-table tbody tr:hover{
  background-color: rgba(80,184,60,0.1);
}.product-content-table tr:active{
  background-color: #E5F3FF;
}
.product-content-table table thead tr th:first-child{
  min-width: 30px;
}

.product-tab-th-select {
  text-align: center !important;
  padding: 0 !important;
  z-index: 1;
}
.product-tab-th-select-amout{
  text-align: right !important;
  padding-left: 10px !important;
  z-index: 1;
}
.product-content-table thead {
  background-color: rgb(236, 238, 241);
}.product-content-table table thead tr th:last-child{
  min-width: 110px;

  background-color: rgb(236, 238, 241);
  border-left: 1px solid rgb(216, 211, 211);
}
.product-content-table table tbody tr td:last-child{
  color: #0075CC;
  display: flex;
  justify-content: center;
  font-weight: 600;
  /*
  position: sticky;
  right: 0; 
  */
  z-index: 1;
  background-color: #fff;

}.product-tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 3px;
  
}.product-tab-th-select-lable{
   margin-top: 16px;
   margin-left: 30px;
   font-weight: 600;
}.product-btnopendrop{
  position: relative;
  width: 5px;
  height: 5px;
  margin-top: 12px;
  margin-left: 30px;

}.product-content-table table thead tr th{
  font-family: Misa Fonts Bold;
  position: sticky;
  top: 0;
  background-color: rgb(236, 238, 241);
  z-index: 3;
  
}
.product-tool-tip-table {
        width: 140px;
        font-size: 11px;
        position: absolute;
        top: 30px;
        right: 0;
        background-color: #505050;
        border-radius: 2px;
        padding: 2px 4px;
        z-index: 3;
        text-align: center;
        color: #fff;
        visibility: hidden;
        
    }
    .product-cmnd:hover .product-tool-tip-table {
        visibility: visible;
        opacity: 1;
    }
</style>

