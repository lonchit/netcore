import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class ExceptionsListComponent extends NucleusComponentBase {
    loading = true;
    search = '';
    options = {};

    pagedListOfExceptionsListDto: IPagedList<IExceptionsListOutput> = {
        totalCount: 0,
        items: []
    };

    mounted() {
        this.getExceptions();
    }

    public getExceptions() {
        this.loading = true;

        this.nucleusService.get<IPagedList<IExceptionsListOutput>>('/api/Exception', false).then((response) => {
            this.pagedListOfExceptionsListDto = response.content as IPagedList<IExceptionsListOutput>;
            this.loading = false;
        });
    }
}