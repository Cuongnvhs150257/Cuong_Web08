<template>
    <div class="padding">
                    <div class="padding-left" v-if="TotalCount">
                        <label>Tổng số: </label>
                        <label class="padding-left-bold">{{TotalCount}}</label>
                        <label> bản ghi</label>
                    </div>
                    <div class="padding-right">
                        <div class="drop-box">
                            <MDropBox @padding-value ="getPaddingValue"/>
                        </div>
                        <div class="padding-center">
                            <button class="btn-first" :class="{ 'disabled' : currentPage === 1 }" @click="PrevValue()" >Trước</button>
                            <div class="page-group" :class="{ 'button-seleced': currentPage === item }" v-for="(item, index) in DisplayValue" :key="index" @click="offSetValue(item)">
                                {{item}}
                            </div>
                            <button class="btn-last" :class="{ 'disabled': currentPage === Math.ceil(TotalCount/indexPadding) }" @click="NextValue()">Sau</button> 
                        </div>
                    </div>
                </div>
</template>

<script>
import MDropBox from "./MDropBox.vue";
export default {
    props:{
        TotalCount: Number,
    },
    components: {
        MDropBox
    },
    methods:{
        /**
         * lấy số lượng bản ghi hiển thị (limit)
         * Nguyễn Văn Cương 06/10/2022
         */
         getPaddingValue(value){
             this.$emit("filter-padding", value);
             this.indexPadding = value;
         },

         /**
          * lấy số trang bản ghi hiển thị (offset)
          * Nguyễn Văn Cương 06/10/2022
          */
         offSetValue(page){
             if(page === "..."){
                if(this.currentPage === Math.ceil(this.TotalCount/this.indexPadding) - 1 || this.currentPage === Math.ceil(this.TotalCount/this.indexPadding)){
                    this.PrevValue();
                }
                else{
                    this.NextValue();
                }
            }
            else{
                this.currentPage = page;
                if(page === Math.ceil(this.TotalCount/this.indexPadding)){
                    this.pageChangeCLick = this.currentPage - 1;
                    this.endPage = true;
                }
                else if(page === Math.ceil(this.TotalCount/this.indexPadding) - 1){
                    this.pageChangeCLick = this.currentPage;
                }
                else{
                    this.pageChangeCLick = 3;
                    this.endPage = false;
                }
                this.$emit("offset-value", (this.indexPadding) * (page - 1));
            }
         },
         PrevValue(){
            if(this.currentPage > 1){
                if(this.currentPage > 3 && this.currentPage < Math.ceil(this.TotalCount/this.indexPadding)){
                    this.pageChangeCLick = this.currentPage - 1;
                }
                this.currentPage -= 1;
                this.endPage = false;
            this.$emit("offset-value", (this.indexPadding) * (this.currentPage - 1));
            }
        },

        NextValue(){
            if(this.currentPage < Math.ceil(this.TotalCount/this.indexPadding)){
                if(this.currentPage >= 3 && this.currentPage < Math.ceil(this.TotalCount/this.indexPadding) - 1){
                    this.pageChangeCLick = this.currentPage + 1;
                }
                this.currentPage += 1;
                this.$emit("offset-value", (this.indexPadding) * (this.currentPage - 1));
            }
        },
        
    }, 
    computed:{
        DisplayValue(){
           let arr = [];
           for (let i = 1; i < Math.ceil(this.TotalCount/this.indexPadding) + 1; i++) {
                    if(i === Math.ceil(this.TotalCount/this.indexPadding)){
                        arr.push(i);
                    }
                    else if(i === 3){
                        if(this.endPage == true){
                            arr.push("...");
                        }else{
                            arr.push(this.pageChangeCLick);
                        }
                    }
                    else if(i < 4){
                        arr.push(i);
                    }
                    else if(i === 4){
                        if(this.endPage == true){
                            arr.push(this.pageChangeCLick);
                        }else{
                            arr.push("...");
                        }
                        
                    }
            }     
            //this.$emit("offset-value", this.currentpage);
            console.log(arr);
            return arr;
            
            
         },
    },
    
    data(){
        return{
            currentPage: 1,
            pageChangeCLick : 3,
            endPage: false,

            indexPadding: 10, //số lượng bản ghi
            indexOffSetOn: 2, //trang hiện tại
            indexOffSetNext: 2, //trang tiếp theo sẽ mở
            seleced: true, //hiện thị trang đang chọn
            seleced2: false, //hiện thị trang đang chọn
            seleced3: false, //hiện thị trang đang chọn
        }
    }
}
</script>

<style scoped>
.padding{
    width: 100%;
    border-left: 2px solid #fff;
    height: 55px;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    order: 5;
    background-color: #fff;
    border-radius: 0px 0px 4px 4px;
}
.padding-center{
    display: flex;
    margin-left: 20px;
    
}.padding-right{
    position: absolute;
    right: 0;
    margin-right: 10px;
    display: flex;
    height: 25px;
    top: 10px;
}.padding-right select{
    width: 200px;
    height: 30px;
}.padding-right select:focus{
    border: 2px solid #019160;
    outline: none;
}
.padding-left{
    position: absolute;
    left: 0;
    margin-left: 10px;
}.padding-left-bold{
    font-weight: bold;
}
.page-group.button-seleced {
    border: 1px solid #bbbb;
    color: #000;
    font-weight: 700;
    width: 19px;
    text-align: center;
    height: 20px;
    background-color: #fff;

}

.btn-first, .btn-last{
    width: 40px;
    height: 30px;
    border: none;
    background-color: #fff;
    cursor: pointer;
    margin-right: 10px;
}
.btn-first.disabled, .btn-last.disabled{    
    color: #666666;
}.drop-box{
    width: 200px;
    height: 33px;
    border: 1px solid #bbbbbb;
    outline: none;
    border-radius: 4px;
}.page-group{
    margin-left: 6px;
    margin-right: 6px;
    margin-top: 5px;
    cursor: pointer;
}
</style>

