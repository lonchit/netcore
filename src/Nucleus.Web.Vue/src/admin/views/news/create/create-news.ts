import { Component } from 'vue-property-decorator';
import NucleusComponentBase from '@/shared/application/nucleus-component-base';

@Component
export default class NewsCreateComponent extends NucleusComponentBase {
    refs = this.$refs as any;
    createNewsInput = {} as ICreateNewsInput;
    errors: INameValueDto[] = [];
    resultMessage: string | undefined;
    creationNewsIsComplete = false;

    onSubmit() {
        if (this.refs.form.validate()) {
            this.nucleusService.post<ICreateNewsOutput>('/api/news/PostNews', this.createNewsInput)
                .then((response) => {
                    if (!response.isError) {
                        this.resultMessage = this.$t('NewsCreationSuccessful').toString();
                        this.creationNewsIsComplete = true;
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }
}