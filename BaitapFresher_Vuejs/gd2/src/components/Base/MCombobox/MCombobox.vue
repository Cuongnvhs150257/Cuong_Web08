<template>
  <div class="droptbl" v-if="ComboClass === 1" :class="{'dropbox-input-green': !inValueChange, 'dropbox-input-green': isShowDropbox }" >
    <input class="dropbox-input product" type="text" :readonly="readonly" :style="width" :maxlength="maxlength" v-model="this.RecordSle[label]" :tabindex="tab" />
    <div :class="{'dropbox-input add' : iconadd}" @click="openPopupEdit"></div>
    <button class="droptbtn" @click="btnDropClick" ref="cover_combobox"><div class="droptbtn-icon" :style="style"></div></button>
    <div class="dropbl-data" v-show="isShowD" ref="combobox">
        <div class="drop-for" v-for="d in DropboxShow" :key="d[value]" :class="{select : d[value] == RecordSle[value] , select : d[label] == RecordSle[label] }" @click="selectedRecord(d)" >
          <div class="drop-itemtbl" >{{d[label]}}</div>
        </div>
    </div>
    
  </div>
  <div class="combomuti" v-if="ComboClass === 2" :class="{'dropbox-input-green': !inValueChange, 'dropbox-input-green': isShowDropbox }">
    <div class="combomuti-input-main">
      <div class="combomuti-input-main-data" v-for="item in ComboMutiItem" :key="item">
        <div class="combomuti-main-item">
          <div class="item-label">{{item}}</div>
          <div class="item-cancel" @click="CancelComboMutiItem(item)" ></div>
        </div>
      </div>
      <input class="combomuti-input" type="text" :readonly="readonly" :style="width" :maxlength="maxlength" v-model="this.RecordSle[label]" :tabindex="tab" />
    </div>
    
    <div :class="{'dropbox-input add' : iconadd}" @click="openPopupEdit"></div>
    <button class="droptbtn" @click="btnDropClick" ref="cover_combobox"><div class="droptbtn-icon" :style="style"></div></button>
    <div class="combomuti-data" v-show="isShowD" :style="width_combomuti" ref="combobox" >
        <div class="combomuti-head">
          <div class="combomuti-head-left">{{CombolabelLeft}}</div>
          <div class="combomuti-head-right">{{CombolabelRight}}</div>
        </div>
        <div class="combomuti-cover">
            <div class="combomuti-for" v-for="d in DropboxShow" :key="d[value]" :class="{select : d[value] == RecordSle[value] , select : d[label] == RecordSle[label] }" @click="selectedMuti(d)" >
              <div class="combomuti-itemtbl" >
                <div class="combomuti-item-left">{{d[code]}}</div>
                <div class="combomuti-item-center">{{d[label]}}</div>
                <div :class="{'combomuti-item-right' : d[label] === ComboMutiItem[indexComboMutiItem]}"></div>
          </div>
        </div>
        
        </div>
    </div>
  </div>
</template>

<script>
import configs from "../../../configs/index";
export default {
  created(){

  },
  mounted() {
    this.BrindingMuti();
    window.addEventListener('mousedown', this.clickEventInterrupt);
  },
  unmounted() {
    window.removeEventListener('mouseup', this.clickEventInterrupt);
  },
  updated(){
    this.ResetCombobox();
  },
    props:{
        DropboxItem: [],
        tab: Number,
        maxlength: Number,
        value: String,
        label: String,
        labelSupply: String,
        code: String,
        valuePost: String,
        valueFilter: String,
        readonly: Boolean,
        iconadd: Boolean,
        width: String,
        Comboboxmodel: String,
        baseURL: String,
        ComboClass: {
          type: Number,
          default: 1,
          
        },
        CombolabelLeft: String,
        CombolabelRight: String,
        width_combomuti: String,
        InputClass: Boolean,
        Reset: Boolean,
        ResetSupply: Boolean,
        Filter: Boolean,
        inputfilter: Number,
        Tabname: String,
        BridingSupplyCode: String,
        BridingSupplyID: String,
    },
    methods:{
        /**
          Hàm reset combobox
          Nguyễn Văn Cương 11/11/2022
         */
         ResetCombobox(){
            if(this.Reset == true){
              this.RecordSle[this.label] = "Tất cả";

              this.RecordSle[this.value] = 0;
              this.$emit("ChangeReset");
            }
            if(this.ResetSupply == true){
              this.ComboMutiItem = [];
              this.$emit("ChangeReset");
            }
         },

        /**
         * hàm chọn đơn vị
         * Nguyễn Văn Cương 05/11/2022
         */
        selectedRecord(drop){
          console.log(drop);
          this.isShowDropbox = !this.isShowDropbox;
          this.isShowD = this.isShowDropbox;
          if(this.Filter == true){
            this.$emit("get-recordvalue", drop[this.label], this.valueFilter, this.Tabname, this.inputfilter);
          }else{
            this.$emit("get-recordvalue", drop[this.value], this.valuePost, this.Tabname, this.inputfilter);
          }
          this.inValueChange = false;
          this.selectItem = false;
          this.RecordSle[this.label] = drop[this.label];
          this.Rotate();
        },

        /**
        hàm mở combobox
        Nguyễn Văn Cương 05/10/2022
         */
        btnDropClick(){ 
          //trường hợp combobox dữ liệu có sẵn
           if(this.DropboxItem){
              this.DropboxShow = this.DropboxItem
           }else{
             //trường hợp combobox cần load dữ liệu
             this.loadUnit();
           }
           this.isShowDropbox = !this.isShowDropbox;
           this.isShowD = this.isShowDropbox; 
           //xoay icon nút 
           this.Rotate();
        },

        /**
        hàm chọn nhiều phần tử
        Nguyễn Văn Cương 05/10/2022
         */
        selectedMuti(muti){
          this.inValueChange = false;
          if(this.InputClass == false){
            //kiểm tra xem mảng chứa đã có chưa
            if(!this.ComboMutiItem.includes(muti[this.code])){
              this.ComboMutiItem.push(muti[this.code]);
            }
            //lưu index của giá trị
            this.indexComboMutiItem = this.ComboMutiItem.indexOf(muti[this.code]);
            console.log(this.ComboMutiItem);
            this.$emit("get-recordvalue", muti[this.value], this.valuePost);
            
          }else{
            //chưa có thì chọn tiếp
            this.selectedRecord(muti);
          }
          //xoay icon nút
          this.Rotate();
          
        },

        BrindingMuti(){
          if(this.BridingSupplyCode){
            if(!this.ComboMutiItem.includes(this.BridingSupplyCode)){
              this.ComboMutiItem.push(this.BridingSupplyCode);
            }
          }
          if(this.BridingSupplyID){
            this.$emit("get-recordvalue", this.BridingSupplyID, this.valuePost);
          }
        },

        /**
        hàm xóa phần tử trong combobox nhiều
        Nguyễn Văn Cương 05/10/2022
         */
        CancelComboMutiItem(value){
          //tìm index của phần từ trong mảng
          const index = this.ComboMutiItem.indexOf(value);
          if (index > -1) {
            //loại bỏ phần tử khỏi mảng
            this.ComboMutiItem.splice(index, 1); 
          }  
        },

        /**
        hàm mở popup thêm mới
        Nguyễn Văn Cương 05/10/2022
         */
        openPopupEdit(){
          this.$emit("open-popup-edit");
        },
       
        /**
         * hàm lấy toàn bộ đơn vị
         * Nguyễn Văn Cương 05/10/2022
         */
        loadUnit(){
            fetch(configs[this.baseURL], {
            method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.DropboxShow = data;
          console.log(data)
        })
        .catch((res) => {
          console.log(res);
        });
        },

      /**
        hàm xoay icon nút
        Nguyễn Văn Cương 05/10/2022
         */  
      Rotate(){
        if(this.isShowD == true){
          this.style = "transition: linear 0.2s; transform: rotate(180deg);"
        }else{
          this.style = "transition: linear 0.2s; transform: rotate(0deg);"
        }
      },
      
      /**
        hàm click outside
        Nguyễn Văn Cương 05/10/2022
         */
      clickEventInterrupt(event){
      if(this.isShowD == true){
        //kiểm tra click có chứa combobox không
        const isClick = this.$refs.combobox.contains(event.target);
        if(!isClick){
          //nếu không đóng combobox
          this.isShowD = false;
          //xoay icon nút
          this.style = "transition: linear 0.2s; transform: rotate(0deg);"
        }
      }
    },
    },
    data(){
      
      var RecordSle= {
          //hiện thị giá trị của bảng trên input
            [this.label]: this.Comboboxmodel 
      };
      return{
        //trạng thái đóng mở combobox
        isShowD: false,
        //trạng thái đóng mở combobox
        isShowDropbox: false,
        //lưu giá trị
        selectItem: true,
        //nổi bật đơn vị đã chọn
        inValueChange: true, 
        //mảng chứa giá trị combobox
        DropboxShow: [],
        //hiển thị giá trị đã chọn lên input
        RecordSle,
        //lưu giá trị combobox nhiều
        ComboMutiItem: [],
        //lưu vị trí value trong mảng
        indexComboMutiItem: Number,
        //lưu thuộc tính style nút icon
        style: {},
        //lưu trạng thái xóa combobox
        sta: true,

      }
    }
}
</script>

<style scoped>
:root{
    --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}.select .drop-itemtbl {
  background-color: #2ca01c !important;
  color: #fff !important;
}
.select .combomuti-itemtbl {
  background-color: #2ca01c !important;
  color: #fff !important;
}
.droptbl{
  height: 31px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
  background-color: #fff;
  margin-top: 5px;
  border-radius: 4px;
  border: 1px solid #bbbbbb;
}

.droptbtn{
  position: absolute;
  border: none;
  right: 1px;
  width: 30px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  background: #fff;
  z-index: 3;
}
.dropbox-input.product{
  width: 80%;
  height: 27px;
  float: left;
  height: 100%;
  border: none;
  outline: none;
  padding-left: 10px;
  border-radius: 4px;
  cursor: pointer;
}.dropbox-input.product:focus ~.droptbl{
  border: 1px solid #50B83C;
  outline: none;
}
.droptbtn-icon{
    background-image: var(--icon);
    background-position: -552px -353px;
    background-repeat: no-repeat;
    width: 30px;
    height: 28px;
}.droptbtn:hover{
    border: none;
    outline: none;
    background-color: #dddd;
}.dropbox-input.add:hover{
    background-color: #dddd;
}
.drop-for{
    width: 100%;
}.dropbl-data{
    position: absolute;
    width: 99%;
    z-index: 6;
    border: 1px solid #bbbb;
    top: 32px;
    border-radius: 4px;
    overflow-y: scroll;
    overflow-x: hidden;
    max-height: 184px;
}.dropbox-input.add{
    position: absolute;
    right: 30px;
    background-image: var(--icon);
    background-position: -24px -305px;
    background-repeat: no-repeat;
    width: 30px;
    height: 28px;
    z-index: 5;
    border-right: 1px solid #bbbb;
    cursor: pointer;
}
.drop-itemtbl{
  height: 32px;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: #fff;
  font-size: 13px;
  font-weight: lighter;
  color: #000;
  z-index: 100;
  padding-left:8px ;
  cursor: pointer;
}
.drop-itemtbl:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.drop-data-hide{
  display: none;
}.dropbox-input-green{
  border: 1px solid #50B83C !important;
}.dropbox-input-red{
  border: 1px solid #ff0000 !important;
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
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #7c869c;
}.combomuti-data{
  position: absolute;
    width: 140%;
    z-index: 6;
    border: 1px solid #bbbb;
    top: 32px;
    border-radius: 4px;
    height: 200px;
}.combomuti-cover{
  overflow-y: scroll;
  overflow-x: hidden;
  max-height: 160px;
}.combomuti-for{
    width: 100%;
}.combomuti-itemtbl{
  height: 32px;
  width: 100%;
  display: flex;
  align-items: center;
  background-color: #fff;
  font-size: 13px;
  font-weight: lighter;
  color: #000;
  z-index: 100;
  padding-left:8px ;
}
.combomuti-itemtbl:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.combomuti-head{
  display: flex;
  height: 30px;
  font-family: Misa Fonts Bold;
  font-size: 13px;
  padding-top: 10px;
  background-color: #f4f5f8;
  position: relative;
}.combomuti-head-right{
  position: absolute;
  left: 50%;
 
}.combomuti-head-left{
  margin-left: 10px;
}.combomuti-itemtbl{
  display: flex;
  position: relative;
}.combomuti-item-center{
  position: absolute;
  left: 50%;
}.combomuti-item-left{
  margin-left: 8px;
}.combomuti-item-right{
  position: absolute;
  right: 0;
  background-image: var(--icon);
  background-position: -899px -316px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
  margin-right: 15px;
}.combomuti-input-main{
  width: 91.5%;
  display: flex;
  flex-wrap: wrap;
  border-right: 1px solid #bbbb;
}.combomuti-main-item{
  border: 1px solid #ccc;
  align-items: center;
  border-radius: 3px;
  background-color: #f0f0f0;
  padding: 2px 3px 2px 5px;
  margin-top: 3px;
  margin-left: 4px;
  min-width: 50px;
  overflow: hidden;
  display: flex;
  cursor: pointer;
}.item-label{
  font-family: Misa Fonts Regular;
  font-size: 11px;
}.combomuti{
  min-height: 31px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
  background-color: #fff;
  margin-top: 5px;
  border-radius: 4px;
  border: 1px solid #bbbbbb;
  align-items: center;
}.combomuti-input{
  min-width: 35%;
  min-height: 27px;
  float: left;
  border: none;
  outline: none;
  padding-left: 10px;
  border-radius: 4px;
  cursor: pointer;
}.item-cancel{
  background-image: var(--icon);
  background-position: -78px -312px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
}
</style>