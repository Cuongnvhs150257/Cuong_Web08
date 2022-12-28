<template>
  <div class="contentner"  id="opendrop" > 
    <table >
    <thead>
      <tr>
        <th v-if="TableCheckBox" class="box"><MCheckbox @click="handleCheckBoxAll" :stateCheckAll="stateCheckAll"  /></th>
        <th v-for="index in thListTable" :key="index.label" :class="index.class" :style="index.style">{{index.label}} <div @click="openFilter(index.inputfilter, index.property, index.filterlabel)" class="filter-header-icon"></div> <span class="tooltip" v-if="index.span">{{index.span}}</span></th>
        <th class="tab-th-select">CHỨC NĂNG</th>
      </tr>
    </thead>
    <tbody v-if="RecordsLoad" >
      <tr
        v-for="re in RecordsLoad"
        :key="re[tdListTable[tdListTable.length - 1].property]"
        @dblclick="rowDBClick(re[tdListTable[tdListTable.length - 1].property])"
      >
        <td v-if="TableCheckBox" class="box"><MCheckbox @click="handleCheckBox(re[tdListTable[tdListTable.length - 1].property])"  :stateCheckAll="stateCheckAll" /></td>
        <td v-for="index in tdListTable" :class="index.class" :style="index.style" :key="index" >{{index.fun === 1 ? this.formatStatusRecord(re[index.property]) 
          : index.fun === 2 ? this.formatGenderRecord(re[index.property])
          : index.fun === 3 ? this.formatDateRecord(re[index.property])
          : index.fun === 4 ? this.formatTaxRecord(re[index.property])
          : index.fun === 5 ? this.formatNatureRecord(re[index.property])
          : index.fun === 6 ? this.SumFormat(re[index.property])
          : re[index.property] }}
          </td>
        <td style="min-width: 110px;">
          
          <label class="tab-th-select-lable" @click="rowDBClick(re[tdListTable[tdListTable.length - 1].property])">Sửa</label>
          <div class="btnopendrop" @click="getRecordDetele(re[tdListTable[tdListTable.length - 1].property], re[tdListTable[1].property])" ><MDropItem @edit-value="openPopupAsk" :MDropSta="1"  :Drstyle="StyleDropbox" @getpostion="getPostion" /></div>
        </td>
      </tr>
    </tbody>
    <tbody v-if="RecordsMuti">
        <tr v-for="re in RecordsMuti" :key="re" :style="re.style" @dblclick="rowDBClick(re.arr[tdListTable[tdListTable.length - 1].property])">
          <td v-if="TableCheckBox" class="box"><MCheckbox @click="handleCheckBox(re.arr[tdListTable[tdListTable.length - 1].property])"  :stateCheckAll="stateCheckAll" /></td>
          <td v-for="index in tdListTable" :style="index.style" :key="index" >
            <div class="mutitable" >
                <div @click="openChild(re, RecordsMuti)" :class="{'mutitable-icon' : !Changeicon && !re.class && re.style, 'mutitable-icon b' : Changeicon && !re.class && re.style}" v-if="index.muti === 1"></div>
                <div :class="{'mutitable-label': re.class && index.muti == 1, 'mutitable-label b': !re.class && index.muti == 1, 'mutitable-label c': !re.class && re.style && index.muti == 1}">
                  {{index.fun === 1 ? this.formatStatusRecord(re.arr[index.property]) 
                  : index.fun === 2 ? this.formatGenderRecord(re.arr[index.property])
                  : index.fun === 3 ? this.formatDateRecord(re.arr[index.property])
                  : index.fun === 4 ? this.formatTaxRecord(re.arr[index.property])
                  : index.fun === 5 ? this.formatNatureRecord(re.arr[index.property])
                  : index.fun === 6 ? this.SumFormat(re.arr[index.property])
                  : re.arr[index.property]}}
                </div>
            </div>
          </td>
          <td style="min-width: 110px;">
            <label class="tab-th-select-lable" @click="rowDBClick(re.arr[tdListTable[tdListTable.length - 1].property])">Sửa</label>
            <div class="btnopendrop" @click="getRecordDetele(re.arr[tdListTable[tdListTable.length - 1].property], re.arr[tdListTable[1].property])"><MDropItem @edit-value="openPopupAsk" :MDropSta="1" :Drstyle="StyleDropbox" @getpostion="getPostion" /></div>
        </td>
        </tr>
    </tbody>
    <tfoot v-if="tfoot">
        <td></td>
        <td>Tổng</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        <td>{{SumFormat(SumQuantity)}}</td>
        <td>{{SumFormat(SumExistent)}}</td>
        <td></td>
        <td></td>
    </tfoot>
  </table>
  <div class="mpopup-ask">
    <MPopupNotification v-if="isShowAskDelete" :record="PopupNotilable" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteRecord" @close-notification-click="ClosePopupAsk"
    :getRecordCode="getrecorddeteteCode" :MPopupN="PopupNotificationMode" />
    
  </div>
  <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>
  </div>
  
</template>

<script>
import MPopupNotification from '../MPopupNotification/MPopupNotification.vue';
import MCheckbox from "../MCheckbox/MCheckbox.vue";
import MToast from '../MToast/MToast.vue';
import MDropItem from "../MDropItem/MDropItem.vue";
import configs from '../../../configs/index';
import enums from '../../../resouce/enums';
import toast from '../../../resouce/toast';
import formatjs from '../../../resouce/format';
import productjs from '../../../resouce/product';

export default {
  name: "RecordList",
  props: {
    //object chứa record
    RecordsLoad: Object,
    //trạng thái đóng mở
    closeSelectedAll: Boolean,
    //table theader
    thListTable: [],
    //table tbody
    tdListTable: [],
    //table tfooter
    tfoot: Boolean,
    //tiêu đề của popup
    PopupNotilable: String,
    //url 
    baseURL: String,
    //trạng thái checkbox 
    TableCheckBox: Boolean,
    //object chứa record đệ quy
    RecordsMuti: Object,
    //object chứa record đệ quy
    CheckDeleteIns: Boolean,
    //Tổng số lượng tồn
    SumQuantity: Number,
    //Tổng giá trị tồn
    SumExistent: Number,
    //trạng thái xóa checkbox
    DeleteCheckbox: Boolean,
  },
  components: {
    MPopupNotification,
    MCheckbox,
    MToast,
    MDropItem,
  },  
  updated() {
    if(this.DeleteCheckbox){
      this.stateCheckAll = false;
    }
  },
  mounted() {
    //gọi hàm click outsite
    window.addEventListener('mousedown', this.clickEventInterrupt);
  },
  unmounted() {
    //xóa hàm click outsite
    window.removeEventListener('mouseup', this.clickEventInterrupt);
  },
  
  methods: {
    
    /**
     * Hàm format tổng
     * Nguyễn Văn Cương 21/11/2022
     */
     SumFormat(price) {
       if(price){
        const pieces = parseFloat(price).toFixed(2).split('')
        let ii = pieces.length - 3
        while ((ii-=3) > 0) {
          pieces.splice(ii, 0, ',')
        }
        return pieces.join('');
       }else{
         return "";
       }

    },

    /**
    Hàm mở td con của table đệ quy
    Nguyễn Văn Cương 10/11/2022
     */
    openChild(value, re){
      //tìm giá trị trong mảng
      const index = this.RecordsMuti.indexOf(value);
      //đóng mở td con
      this.openChildMuti = !this.openChildMuti;
      //lưu style hiển thị
      const style = "none";
      const style1 = "display: none";
      if(this.openChildMuti){
        re[index + 1].style = style;
        this.Changeicon = true;
      }else{
        re[index+ 1].style = style1;
        this.Changeicon = false;
      }
      
    },

    /**
    * hàm click outsite
    * Nguyễn Văn Cương 01/10/2022
    */
    clickEventInterrupt(event){
    //lưu vị trí con chuột left, top
      this.PosY = event.y;
      this.PosX = event.x
    },

    /**
    Hàm gọi popup Filter
    Nguyễn Văn Cương 10/11/2022
     */
    openFilter(inputfil , value, filterlabel){
      this.$emit("Show-Filter", 2 , inputfil, value, filterlabel, this.PosY, this.PosX);
    },

    /**
    Hàm lấy vị trí để hiển thị dropbox
    Nguyễn Văn Cương 10/11/2022
     */
    getPostion(DropboxLeft, DropboxTop){
      //vị trí cách trái
      DropboxLeft = DropboxLeft - 300;
      //vị trí cách trên
      DropboxTop = DropboxTop - 36;
        this.StyleDropbox = "left: " + DropboxLeft + "px;" + "top: " + DropboxTop + "px;";
    },

    /**
     * Hàm thực hiện kích hoạt toàn bộ checkbox
     * Nguyễn Văn Cương 15/10/2022
     */
    handleCheckBoxAll(){
      //chuyển trạng thái checkbox
      this.stateCheckAll = !this.stateCheckAll;
      if(this.stateCheckAll == true){
          //vòng lặp thêm mã nhân viên vào mảng
          this.RecordsLoad.forEach(re => {
            this.listReSelected.push(re[this.tdListTable[this.tdListTable.length - 1].property]);
          },
          this.$emit("get-List-Checkbox", this.listReSelected, this.stateCheckAll)
          );
      }else{
        this.listReSelected = [];
        this.$emit("get-List-Checkbox", this.listReSelected, this.stateCheckAll)
      }
      console.log(this.listReSelected);
    },

    /**
     * hàm thực hiện kích hoạt checkbox
     */
    handleCheckBox(ReID){
          //thêm mã nhân viên đã chọn vào mảng
          if(!this.listReSelected.includes(ReID)){
            this.listReSelected.push(ReID);
          }
          else{
            const index = this.listReSelected.indexOf(ReID);
            if (index > -1) {
              //loại bỏ phần tử khỏi mảng
              this.listReSelected.splice(index, 1); 
            }
          }
          if(this.listReSelected.length == 10){
            this.handleCheckBoxAll();
          }else{
             this.stateCheckAll = false;
            this.$emit("get-List-Checkbox", this.listReSelected, this.stateCheckAll);
          }
          
    },

    /**
     * hàm hiện thông tin trên popup khi nhấn vào Sửa
     * Nguyễn Văn Cương 25/09/2022
     */
    rowDBClick(recordID) {
      //bấm dbclick để sửa
      this.detailFormMode = 2;
      this.$emit("custom-open-dbclick", recordID, this.detailFormMode);
    },

    /**
     * hàm lấy thông tin nhân viên khi xóa
     * Nguyễn Văn Cương 25/09/2022
     */
    getRecordDetele(recordID, recordCode){
        this.getrecorddetetevalue = recordID;
        this.getrecorddeteteCode = "<" + recordCode + ">";
        
    },
 
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(selectedit){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.PopupNotificationMode = 2;
            this.idRecordDelete = this.getrecorddetetevalue; //lưu id employee cần xóa
        }else if(this.checkDelete == 1){
          //bấm nhân bản
           this.detailFormMode = 1;
           this.$emit("custom-open-dbclick", this.getrecorddetetevalue, this.detailFormMode);
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
       this.popupAskCance = true; //lưu trạng thái đóng popup hỏi người dùng
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
     * hàm format ngày tháng 
     * Nguyễn Văn Cương 01/10/2022
     */
    formatDateRecord(date) {
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
     * hàm format giới tính 
     * Nguyễn Văn Cương 01/10/2022
     */
    formatGenderRecord(gender){

      //giá trị 1 là nữ 
       if(gender == enums.FEMALE){
         return gender = formatjs.Gender_Female;
      //giá trị 2 là nam
       }else if(gender == enums.MALE){
         return gender = formatjs.Gender_Male;
       //giá trị 0 là khác
       }else if (gender == enums.ELSE){
         return gender = formatjs.Gender_Else;
      //không có cho thành rỗng
       }else{
         return gender = "";
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
     * Hàm xóa employee theo id 
     * Nguyễn Văn Cương 25/09/2022
     */
    async deleteRecord() {
      try {
        this.isShowToast = false;
          //lấy employeeid đã lưu 
          var id = this.idRecordDelete; 
            //đóng popup hỏi người dùng
            this.ClosePopupAsk();
          var Incurred = true;
            if(this.CheckDeleteIns){
              await this.CheckDelete(id);
              if(this.IncurredID != null){
                Incurred = false;
                this.isShowAskDelete = true;
                this.PopupNotificationMode = 4;
              }
            }
            if(Incurred){
              await fetch(
            configs[this.baseURL] + id,
            { method: "DELETE" })
            .then((res) => res.json())
            .then((data) => {
              if (data.errorCode) {
              //mở popup thông báo
              this.ShowToast(this.ToastStatus = false);
              }
              else{
                  //load lại data
                if(this.RecordsLoad.length <= 1){
                  this.$emit("get-offset-delete");
                }else{
                  this.$emit("data-load-delete");
                }
                this.ShowToast(this.ToastStatus = true);
                console.log(data);
              }
            })
            .catch((res) => {
              this.ShowToast(this.ToastStatus = false);
              console.log(res);
            });  
            }
      } catch (error) {
        console.log(error);
      }
    },

    /**
    Hàm check phát sinh khi xóa
    Nguyễn Văn Cương 15/11/2022
     */
    async CheckDelete(recordid){
      try {
        this.IncurredID = null;
        await fetch(configs[this.baseURL] + productjs.CheckDelete + recordid, {method: "GET"})
        .then((res) => res.json())
        .then((data) => {
          if (data.errorCode) {
            //mở popup thông báo
            this.ShowToast(this.ToastStatus = false);
          }
          else{
            this.IncurredID = JSON.stringify(data);
          }
        })
      } catch (error) {
        console.log(error);
      }
    },


    
  },
  data() {
    return {
      //lưu dữ liệu nhân viên
      records: [], 
       //lưu nhân viên đã chọn
      reSelected: {},
      //lưu trạng thái mở popup
      detailFormMode: 1, 
       //gọi popup hỏi có xóa không
      isShowAskDelete: false,
      //nút hủy xóa
      popupAskCance: false, 
      //lưu id nhân viên cần xóa
      idRecordDelete:0, 
      //trạng thái xóa
      checkDelete: 2,  
       //lưu id nhân viên cần xóa
      getrecorddetetevalue: 0,
      //lưu code nhân viên cần xóa
      getrecorddetetecode: '', 
      //lưu trạng thái checkbox
      stateCheckAll: false, 
      //lưu danh sách nhân viên cần xóa
      listReSelected: [], 
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
      //lưu vị trí của dropbox
      StyleDropbox:{}, 
      //thay đổi trạng thái icon mở td con
      Changeicon: false,
      //thay đổi trạng thái mở td con
      openChildMuti: false,
      //lưu phát sinh
      IncurredID: null,
      //kiểu mở popup thông báo
      PopupNotificationMode: 2,
      //Tổng số lượng tồn
      SumQuantityV1: null,
      //Tổng giá trị tồn
      SumExistentV1: null,


    };
  },
};
</script>

<style>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}

.contentner{
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
.content-table table {
  width: 100%;
  border-spacing: unset;
  font-size: 13px;

}.product-tab-th-amount{
  text-align: right !important;
  padding-right: 8px !important;
}

td, th {
  border-bottom: 1px solid rgb(216, 211, 211);
  border-right: 1px solid rgb(216, 211, 211);
  height: 42px;
  font-size: 13px;

}
.content-table tr td,
.content-table tr th {
  padding-left: 8px;
  text-align: left;
}.content-table tbody tr:hover{
  background-color: rgba(80,184,60,0.1);
}.content-table tr:active{
  background-color: #E5F3FF;
}
.content-table table thead tr th:first-child{
  min-width: 30px;
}

.tab-th-select {
  text-align: center !important;
  padding: 0 !important;
  z-index: 1;
}
.tab-th-amount{
  text-align: right !important;
  z-index: 1;
}
.content-table thead {
  background-color: rgb(236, 238, 241);
}.content-table table thead tr th:last-child{
  min-width: 110px;

  background-color: rgb(236, 238, 241);
  border-left: 1px solid rgb(216, 211, 211);
}
.content-table table tbody tr td:last-child{
  color: #0075CC;
  display: flex;
  justify-content: center;
  font-weight: 600;
  position: sticky;
  right: 0; 
  border-left: 1px solid rgb(216, 211, 211);
  z-index: 1;
  background-color: #fff;

}
.content-table table tbody tr td:last-child:has(.mdrop-datatbl){
  z-index: 2;
}

.content-table table thead tr th:last-child{
  position: sticky;
  right: 0; 
  border-left: 1px solid rgb(216, 211, 211);
}
.tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 3px;
  
}.tab-th-select-lable{
   margin-top: 16px;
   margin-left: 30px;
   font-weight: 600;
   cursor: pointer;
}.btnopendrop{
  position: relative;
  width: 30px;
  height: 30px;
  margin-top: 12px;

}.content-table table thead tr th{
  font-family: Misa Fonts Bold;
  position: sticky;
  top: 0;
  background-color: rgb(236, 238, 241);
  z-index: 3;
  font-size: 12px;
  
}.tooltip{
  width: 140px;
  font-size: 11px;
  position: absolute;
  top: 30px;
  right: 0;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 5;
  text-align: center;
  color: #fff;
  visibility: hidden;
}
.tab:hover .tooltip{
  visibility: visible;
  opacity: 1;
  width: 313px;
  font-size: 10px;
  top: 45px;
  right: -70px;
  font-family: Misa Fonts Semibold;
  height: 15px;
}.tab-b:hover .tooltip{
  visibility: visible;
  opacity: 1;
  font-family: Misa Fonts Semibold;
  width: 150px;
  font-size: 10px;
  right: -5px;
  top: 45px;
  height: 15px;
}.cmnd:hover .tooltip{
  visibility: visible;
  opacity: 1;
}.content-table table tfoot{
  background-color: rgb(236, 238, 241);
  height: 30px;
  width: 100%;
  z-index: 4;
  position: sticky;
  bottom: 0;
}.content-table table tfoot td{
  height: 30px;
  font-family: Misa Fonts Bold;
  border: none;
}.mutitable{
  display: flex;
  align-items: center;
}
.mutitable-icon{
  background-image: var(--icon);
  background-position: -605px -305px;
  background-repeat: no-repeat;
  width: 30px;
  height: 30px;
}.mutitable-label{
  padding-left: 50px;
}.mutitable-icon.b{
  background-position: -556px -305px;
}.mutitable-label.b{
  padding-left: 28px;
}.mutitable-label.c{
  padding-left: 0;
}.filter-header-icon{
  background-image: var(--icon);
  background-position: -1688px -565px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
  float: right;
  margin-right: 5px;
  visibility: hidden;
  cursor: pointer;
}.content-table table thead tr th:hover .filter-header-icon{
  visibility: visible;
}
</style>

