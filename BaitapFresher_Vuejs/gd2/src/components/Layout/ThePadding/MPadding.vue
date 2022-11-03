<template>
    <div class="padding">
                    <div class="padding-left" v-if="TotalCount">
                        <label>Tổng số: </label>
                        <label class="padding-left-bold">{{TotalCount.totalCount}}</label>
                        <label> bản ghi</label>
                    </div>
                    <div class="padding-right">
                        <div class="drop-box">
                            <MDropBox @padding-value ="getPaddingValue"/>
                        </div>
                        <div class="padding-center">
                            <button class="btn-first" @click="PrevValue()" >Trước</button>
                            <div class="page-group" v-for="(item, index) in DisplayValue" :key="index" @click="offSetValue(item)">
                                {{item}}
                            </div>
                            <button class="btn-last" @click="NextValue()">Sau</button>
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
                }
                else if(page === Math.ceil(this.TotalCount/this.indexPadding) - 1){
                    this.pageChangeCLick = this.currentPage;
                }
                else{
                    this.pageChangeCLick = 3;
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
           for (let i = 1; i < Math.ceil(this.TotalCount/this.indexPadding); i++) {
               if(this.currentpage === Math.ceil(this.TotalCount/this.indexPadding) - 1 || this.currentpage === Math.ceil(this.TotalCount/this.indexPadding)){
                   if(this.currentpage === Math.ceil(this.TotalCount/this.indexPadding)){
                        arr.push(i);
                   }else if(i === 3){
                        if(Math.ceil(this.TotalCount/this.indexPadding) === 4){
                            arr.push(i);
                        }else{
                            arr.push("...");
                        }
                    }else if(i < 4){
                        arr.push(i);
                    }else if(i === 4){
                        arr.push(this.pageChangeCLick);
                    } 
               }
               else{
                    if(i === Math.ceil(this.TotalCount/this.indexPadding)){
                        arr.push(i);
                    }
                    else if(i === 3){
                        arr.push(this.pageChangeCLick);
                    }
                    else if(i < 4){
                        arr.push(i);
                    }
                    else if(i === 4){
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
            currentpage: 1,
            pageChangeCLick : 3,

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

<style>
.padding{
    width: 100%;
    border-left: 2px solid #fff;
    height: 48px;
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
.page-group button{
    border: 1px solid #000;
    height: 25px;
    background-color: #fff;
}
.page-group button:hover{
    background-color: #fff;
}
.page-group .paddingnumber.button-seleced {
    font-weight: bold;
    border: 1px solid #bbbb;

}
.page-group-last .paddingnumber.button-seleced {
    font-weight: bold;
    border: 1px solid #bbbb;

}
.page-group .paddingnumber, .page-group-last .paddingnumber{
    margin-top: 5px;
    margin-left: 3px;
    width: 20px;
    height: 20px;
    margin-right: 5px;
    padding-top: 2px;
    cursor: pointer;
    border: none;
    outline: none;
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
.btn-first:hover, .btn-last:hover{
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

