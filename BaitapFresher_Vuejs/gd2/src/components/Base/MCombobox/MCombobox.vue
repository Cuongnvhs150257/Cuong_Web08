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
      <div class="combomuti-input-main-data" v-for="(item, index) in ComboMutiItem" :key="item">
        <div class="combomuti-main-item">
          <div class="item-label">{{item}}</div>
          <div class="item-cancel" @click="CancelComboMutiItem(item, index)" ></div>
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
        Defaul: Boolean,
    },
    methods:{
        /**
          H??m reset combobox
          Nguy???n V??n C????ng 11/11/2022
         */
         ResetCombobox(){
           //reset c??c combobox th?????ng
            if(this.Reset == true){
              if(this.Filter){
                this.RecordSle[this.label] = "T???t c???";
                this.RecordSle[this.value] = 0;
                this.$emit("ChangeReset");
              }else{
                this.RecordSle[this.label] = "";
                this.RecordSle[this.value] = null;
                this.$emit("ChangeReset");
              }
              
            }
            //reset combobox nh??m v???t t??
            if(this.ResetSupply == true){
              if(this.Filter){
                this.ComboMutiItem = [];
                this.$emit("ChangeReset");
              }
              else{
                this.BrindingMuti();
                this.$emit("ChangeReset");
              }
              
            }
         },

        /**
         * h??m ch???n ????n v???
         * Nguy???n V??n C????ng 05/11/2022
         */
        selectedRecord(drop){
          console.log(drop);
          this.isShowDropbox = !this.isShowDropbox;
          this.isShowD = this.isShowDropbox;
          if(this.Filter == true){
            this.$emit("get-recordvalue", drop[this.code], this.valueFilter, this.Tabname, this.inputfilter);
          }else{
            this.$emit("get-recordvalue", drop[this.value], this.valuePost, this.Tabname, this.inputfilter);
          }
          this.inValueChange = false;
          this.selectItem = false;
          this.RecordSle[this.label] = drop[this.label];
          this.Rotate();
        },

        /**
        h??m m??? combobox
        Nguy???n V??n C????ng 05/10/2022
         */
        btnDropClick(){ 
          //tr?????ng h???p combobox d??? li???u c?? s???n
           if(this.DropboxItem){
              this.DropboxShow = this.DropboxItem
           }else{
             //tr?????ng h???p combobox c???n load d??? li???u
             this.loadUnit();
           }
           this.isShowDropbox = !this.isShowDropbox;
           this.isShowD = this.isShowDropbox; 
           //xoay icon n??t 
           this.Rotate();
        },

        /**
        h??m ch???n nhi???u ph???n t???
        Nguy???n V??n C????ng 05/10/2022
         */
        selectedMuti(muti){
          this.inValueChange = false;
          if(this.InputClass == false){
            //ki???m tra xem m???ng ch???a ???? c?? ch??a
            if(!this.ComboMutiItem.includes(muti[this.code])){
              this.ComboMutiItem.push(muti[this.code]);
            }
            //l??u index c???a gi?? tr???
            this.indexComboMutiItem = this.ComboMutiItem.indexOf(muti[this.code]);
            console.log(this.ComboMutiItem);
            this.BridingIDMuti(muti[this.value]);
          }else{
            //ch??a c?? th?? ch???n ti???p
            this.selectedRecord(muti);
          }
          //xoay icon n??t
          this.Rotate();
          
        },

        /**
         * L???y id c???a c??c nh??m v???t t?? h??ng h??a
         * Nguy???n V??n C????ng 21/11/2022
         */
        BridingIDMuti(value){
          this.SaveID = value;
          if(!this.MutiID.includes(this.SaveID)){
              this.MutiID.push(this.SaveID);
          }
          this.$emit("get-recordvalue",this.MutiID, this.valuePost);
        },

        /**
        H??m hi???n th??? code nh??m v???t t?? h??ng h??a khi m??? popup
        Nguy???n V??n C????ng 21/11/2022
         */
        BrindingMuti(){
          if(this.BridingSupplyCode){
            if(!this.ComboMutiItem.includes(this.BridingSupplyCode)){
              this.ComboMutiItem = this.BridingSupplyCode;
            }
            if(this.BridingSupplyID && this.SaveID == null){
              this.$emit("get-recordvalue", this.BridingSupplyID, this.valuePost);
              for (let i = 0; i < this.BridingSupplyID.length; i++) {
                this.BridingIDMuti(this.BridingSupplyID[i]);
              }
            }
          }
          if(this.Defaul && !this.Comboboxmodel){
            this.RecordSle[this.label] = "Ch??a x??c ?????nh";
            this.$emit("get-recordvalue", 0, this.valuePost, this.Tabname, this.inputfilter); 
          }

        },

        /**
        h??m x??a ph???n t??? trong combobox nhi???u
        Nguy???n V??n C????ng 05/10/2022
         */
        CancelComboMutiItem(value, pos){
          //t??m index c???a ph???n t??? trong m???ng
          const index = this.ComboMutiItem.indexOf(value);
          if (index > -1) {
            //lo???i b??? ph???n t??? kh???i m???ng
            this.ComboMutiItem.splice(index, 1); 
          } 
          //t??m index c???a ph???n t??? trong m???ng
          const post = this.MutiID.indexOf(pos);
          if (index > -1) {
            //lo???i b??? ph???n t??? kh???i m???ng
            this.MutiID.splice(post, 1); 
          } 
        },

        /**
        h??m m??? popup th??m m???i
        Nguy???n V??n C????ng 05/10/2022
         */
        openPopupEdit(){
          this.$emit("open-popup-edit");
        },
       
        /**
         * h??m l???y to??n b??? ????n v???
         * Nguy???n V??n C????ng 05/10/2022
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
        h??m xoay icon n??t
        Nguy???n V??n C????ng 05/10/2022
         */  
      Rotate(){
        if(this.isShowD == true){
          this.style = "transition: linear 0.2s; transform: rotate(180deg);"
        }else{
          this.style = "transition: linear 0.2s; transform: rotate(0deg);"
        }
      },
      
      /**
        h??m click outside
        Nguy???n V??n C????ng 05/10/2022
         */
      clickEventInterrupt(event){
      if(this.isShowD == true){
        //ki???m tra click c?? ch???a combobox kh??ng
        const isClick = this.$refs.combobox.contains(event.target);
        if(!isClick){
          //n???u kh??ng ????ng combobox
          this.isShowD = false;
          //xoay icon n??t
          this.style = "transition: linear 0.2s; transform: rotate(0deg);"
        }
      }
    },
    },
    data(){
      
      var RecordSle= {
          //hi???n th??? gi?? tr??? c???a b???ng tr??n input
            [this.label]: this.Comboboxmodel 
      };
      return{
        //tr???ng th??i ????ng m??? combobox
        isShowD: false,
        //tr???ng th??i ????ng m??? combobox
        isShowDropbox: false,
        //l??u gi?? tr???
        selectItem: true,
        //n???i b???t ????n v??? ???? ch???n
        inValueChange: true, 
        //m???ng ch???a gi?? tr??? combobox
        DropboxShow: [],
        //hi???n th??? gi?? tr??? ???? ch???n l??n input
        RecordSle,
        //l??u gi?? tr??? combobox nhi???u
        ComboMutiItem: [],
        //l??u v??? tr?? value trong m???ng
        indexComboMutiItem: Number,
        //l??u thu???c t??nh style n??t icon
        style: {},
        //l??u tr???ng th??i x??a combobox
        sta: true,
        //l??u l???i id khi ch???n
        SaveID: null,
        //m???ng nhi???u ph???n t??? khi ch???n
        MutiID: [],
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
  min-width: 33px;
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