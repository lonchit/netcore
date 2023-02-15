import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class ListComponent extends NucleusComponentBase {
    loading = true;
    search = '';
    options = {};

    pagedListOfNewsListDto: IPagedList<INewsListOutput> = {
        totalCount: 0,
        items: []
    };

    mounted() {
        this.getNews();
    }

    public getNews() {
        this.loading = true;

        this.nucleusService.get<IPagedList<INewsListOutput>>('/api/News/GetNews', false).then((response) => {
            this.pagedListOfNewsListDto = response.content as IPagedList<INewsListOutput>;
            this.loading = false;
        });
    }
}